using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Xml;
using XMLData;

namespace Game1
{
    public class Game1 : Game
    {
        int screenWidth;
        int screenHeight;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        CollisionCheck collisionCheck;
        Player player;
        PlayerFocus playerFocus;
        Area area;
        Camera camera;
        SpriteFont font;
        ContentManager AreaContent;
        Vector2 velocity;
        Controls controls;
        InteractionBox interactionBox;
        Matrix scaleMatrix;
        bool FullScreen;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            AreaContent = new ContentManager(Content.ServiceProvider);
            AreaContent.RootDirectory = "Content";
            TargetElapsedTime = TimeSpan.FromSeconds(1d / 60d);

        }
        protected override void Initialize() { base.Initialize(); }

        protected override void LoadContent()
        {
            this.IsMouseVisible = true;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //FullScreen = true;
            if (FullScreen)
            {
                screenWidth = GraphicsDevice.DisplayMode.Width / 4;
                screenHeight = GraphicsDevice.DisplayMode.Height / 4;
                graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
                graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
                scaleMatrix = Matrix.CreateScale(4, 4, 1);
            }
            else
            {
                screenWidth = GraphicsDevice.DisplayMode.Width / 4;
                screenHeight = GraphicsDevice.DisplayMode.Height / 4;
                graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width / 2;
                graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height / 2;
                scaleMatrix = Matrix.CreateScale(2, 2, 1);
            }
            graphics.ApplyChanges();

            controls = new Controls();
            camera = new Camera();
            font = Content.Load<SpriteFont>("TestFont");
            interactionBox = new InteractionBox(Content, GraphicsDevice, font);
            interactionBox.Position = new Vector2(100, 100);
            interactionBox.interactions = new InteractionBox.Interaction[] { InteractionBox.Interaction.Check, InteractionBox.Interaction.PickUp };
            //interactionBox.Display = true;

            //Player
            Sprite mainCharacter = new Sprite(Content.Load<Texture2D>(@"character"));
            player = new Player(mainCharacter, 2f);
            player.Position = new Vector2(screenWidth / 2, screenHeight / 2);
            collisionCheck = new CollisionCheck(player);

            playerFocus = new PlayerFocus(Content);

            area = AreaContent.Load<Area>("AreaTest");
            area.Load(AreaContent);
            area.Build();


            //Area testData;
            //testData = new Area();

            //testData.backGroundName = "BackGroundTest";
            //testData.Size = new Vector2(3200, 3200);
            //CollisionRect c1 = new CollisionRect();
            //c1.Rect = new Rectangle(0, 0, 3200, 64);
            //testData.CollisionRects.Add(c1);
            //CollisionRect c2 = new CollisionRect();
            //c2.Rect = new Rectangle(0, 65, 64, 3200);
            //testData.CollisionRects.Add(c2);
            //Building b = new Building();
            //b.transRect = new TransitionRect();
            //testData.Buildings.Add(b);
            //TransitionRect t = new TransitionRect();
            //t.AreaName = "Hej";
            //t.Rect = new Rectangle(1, 2, 3, 5);
            //testData.TransitionRects.Add(t);
            //xmlTest testData = new xmlTest();
            //testData.spawnPosition = xmlTest.SpawnPosition.Below;

            //XmlWriterSettings settings = new XmlWriterSettings();
            //settings.Indent = true;

            //using (XmlWriter writer = XmlWriter.Create("test.xml", settings))
            //{
            //    Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate.IntermediateSerializer.Serialize(writer, testData, null);
            //}
        }
        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            controls.Update();
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) { Exit(); }
            #region Check transition. If transition: perform
            TransitionRect transRect = area.TransitionIntersection(collisionCheck.colRect);
            if (transRect != null)
            {
                AreaContent.Unload();
                area = transRect.loadArea(AreaContent);
                camera.CalculatePotentialOffset(area, screenWidth, screenHeight);
                area.Translate(camera.OffSet);
                foreach (TransitionRect t in area.TransitionRects)
                {
                    if (t.ID == transRect.ID)
                    {
                        transRect = t;
                        break;
                    }
                }
                area.Translate(camera.CenterPosition(transRect.playerPosition(player.PlayerSprite.Texture.Width, player.PlayerSprite.Texture.Height), area, screenWidth, screenHeight));
                area.Translate(camera.Correction(area, screenWidth, screenHeight));
                player.Position = transRect.playerPosition(player.PlayerSprite.Texture.Width, player.PlayerSprite.Texture.Height);
            }
            #endregion
            if (controls.ActionButtonPressed())
            {
                CollisionRect cR = playerFocus.getFocus(area);
                #region Find cR Reference
                if(cR != null)
                {
                    if (cR.getBlock() != null)
                    {
                        interactionBox.DisplayBox(GraphicsDevice, cR.getBlock().interactions, cR.Position, cR.getBlock().checkText);
                    }
                    else if (cR.getBuilding() != null)
                    {
                        interactionBox.DisplayBox(GraphicsDevice, cR.getBuilding().interactions, cR.Position, cR.getBuilding().checkText);
                    }
                }

                #endregion
            }
            interactionBox.Update(GraphicsDevice);

            #region Player-movement
            /*velocity is set, in regard to the player and the current area, taking moving 
             *direction and collision detection into account. 
             *It is used to get the player to move or the area. All actual movement is done 
             *in the Camera class.*/
            velocity = controls.getVelocityWASD();
            player.Update(velocity);
            int currentSpeed = player.getCurrentSpeed();

            if (!controls.diagonalVelocity(velocity))
            {
                velocity *= (int)currentSpeed;
            }
            else
            {
                velocity *= (float)Math.Sqrt(((int)currentSpeed * (int)currentSpeed) / 2);
                velocity = controls.RoundVelocityToFastestInt(velocity);
            }
            velocity = collisionCheck.Move(area, velocity);
            camera.Move(velocity, area, player, currentSpeed, screenWidth, screenHeight);
            player.ZDepth = ((player.Position.Y + player.PlayerSprite.Texture.Height) / 10000) + 0.01f;
            area.Update(collisionCheck.colRect);
            #endregion

            playerFocus.SetFocus(camera, area, player);
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, scaleMatrix);
            player.Draw(spriteBatch);
            area.Draw(spriteBatch);
            spriteBatch.End();

            //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, scaleMatrix);
            playerFocus.Draw(spriteBatch);
            interactionBox.Draw(spriteBatch);
            //spriteBatch.DrawString(font, player.Position.ToString(), new Vector2(50, 50), Color.Red, 0, Vector2.Zero, 1, SpriteEffects.None, 0.9f);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
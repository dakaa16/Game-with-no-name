using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLData
{
    public class InteractionBoxGraphic
    {
        public bool Display { get; set; }
        public InteractionBox.Interaction currentInteraction { get; set; }
        public Vector2 Position { get; set; }
        public string Text { get; set; }
        public int NumberOfInteractions { get; set; }
        private AnimatedSprite[] frameSprites;
        private Sprite[] Sprites;
        private FontGraphics fontGraphics;
        private int frameDimensionX;
        private int frameDimensionY;
        Texture2D DarkEdgeTexture;
        Texture2D EdgeTexture;

        public InteractionBoxGraphic(ContentManager content, GraphicsDevice graphics, SpriteFont font)
        {
            Texture2D CornerTexture = content.Load<Texture2D>("InteractionFrame");
            DarkEdgeTexture = content.Load<Texture2D>("frameDarkEdge");
            EdgeTexture = content.Load<Texture2D>("frameEdge");

            frameDimensionX = CornerTexture.Width / 3;
            frameDimensionY = CornerTexture.Height / 1;
            frameSprites = new AnimatedSprite[8];
            Sprites = new Sprite[4];
            frameSprites[0] = new AnimatedSprite(CornerTexture, Vector2.Zero, 1, 3, 0);
            frameSprites[1] = new AnimatedSprite(CornerTexture, Vector2.Zero, 1, 3, 0);
            frameSprites[1].SpriteEffect = SpriteEffects.FlipHorizontally;
            frameSprites[2] = new AnimatedSprite(CornerTexture, Vector2.Zero, 1, 3, 0);
            frameSprites[2].SpriteEffect = SpriteEffects.FlipVertically;
            frameSprites[3] = new AnimatedSprite(CornerTexture, Vector2.Zero, 1, 3, 1);
            frameSprites[4] = new AnimatedSprite(CornerTexture, Vector2.Zero, 1, 3, 2);
            frameSprites[4].Alpha = 0.5f;
            frameSprites[5] = new AnimatedSprite(CornerTexture, Vector2.Zero, 1, 3, 2);
            frameSprites[5].SpriteEffect = SpriteEffects.FlipHorizontally;
            frameSprites[5].Alpha = 0.5f;
            frameSprites[6] = new AnimatedSprite(CornerTexture, Vector2.Zero, 1, 3, 2);
            frameSprites[6].SpriteEffect = SpriteEffects.FlipVertically;
            frameSprites[6].Alpha = 0.5f;
            frameSprites[7] = new AnimatedSprite(CornerTexture, Vector2.Zero, 1, 3, 2);
            frameSprites[7].SpriteEffect = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
            frameSprites[7].Alpha = 0.5f;
            fontGraphics = new FontGraphics(graphics, font, "None", new Color(23, 27, 53), Vector2.Zero, 20, 20);
            Display = false;
            currentInteraction = InteractionBox.Interaction.None;
            Position = Vector2.Zero;
        }

        public void Update(GraphicsDevice graphics)
        {
            frameSprites[0].Position = Position;
            frameSprites[4].Position = Position;
            fontGraphics.Position = new Vector2(Position.X + frameDimensionX, Position.Y + frameDimensionY);
            fontGraphics.Text = Text;
            fontGraphics.Update(graphics);
            frameSprites[1].Position = new Vector2(fontGraphics.Position.X + fontGraphics.texture.Width, fontGraphics.Position.Y - frameDimensionY);
            frameSprites[5].Position = frameSprites[1].Position;
            frameSprites[2].Position = new Vector2(Position.X, fontGraphics.Position.Y + fontGraphics.texture.Height);
            frameSprites[6].Position = frameSprites[2].Position;
            frameSprites[3].Position = new Vector2(frameSprites[1].Position.X, frameSprites[2].Position.Y);
            frameSprites[7].Position = frameSprites[3].Position;
            Color[] data = new Color[DarkEdgeTexture.Width * DarkEdgeTexture.Height];
            Color[] data1 = new Color[EdgeTexture.Width * EdgeTexture.Height];
            Texture2D tex = new Texture2D(graphics, DarkEdgeTexture.Width, fontGraphics.texture.Height);
            Texture2D tex1 = new Texture2D(graphics, EdgeTexture.Width, fontGraphics.texture.Height);
            Texture2D tex2 = new Texture2D(graphics, DarkEdgeTexture.Width, fontGraphics.texture.Width);
            Texture2D tex3 = new Texture2D(graphics, EdgeTexture.Width, fontGraphics.texture.Width);
            DarkEdgeTexture.GetData<Color>(data);
            var list = new List<Color>();
            for (int i = 0; i < fontGraphics.texture.Height; i++)
            {
                list.AddRange(data);
            }
            tex.SetData(list.ToArray());
            Sprites[0] = new Sprite(tex);
            Sprites[0].Position = new Vector2(Position.X, Position.Y + frameDimensionY);

            EdgeTexture.GetData<Color>(data1);
            list.Clear();
            for (int i = 0; i < fontGraphics.texture.Height; i++)
            {
                list.AddRange(data1);
            }
            tex1.SetData(list.ToArray());
            Sprites[1] = new Sprite(tex1);
            Sprites[1].Position = new Vector2(frameSprites[1].Position.X, Position.Y + frameDimensionY);

            
            list.Clear();
            for (int i = 0; i < fontGraphics.texture.Width; i++)
            {
                list.AddRange(data);
            }
            tex2.SetData(list.ToArray());
            Sprites[2] = new Sprite(tex2);
            Sprites[2].Position = new Vector2(Position.X + frameDimensionX, Position.Y + frameDimensionY);
            Sprites[2].Rotation = -((float)Math.PI / 2.0f);
            Sprites[2].SpriteEffect = SpriteEffects.FlipHorizontally;

            list.Clear();
            for (int i = 0; i < fontGraphics.texture.Width; i++)
            {
                list.AddRange(data1);
            }
            tex3.SetData(list.ToArray());
            Sprites[3] = new Sprite(tex3);
            Sprites[3].Position = new Vector2(Position.X + frameDimensionX, fontGraphics.Position.Y + fontGraphics.texture.Height + frameDimensionY);
            Sprites[3].Rotation = -((float)Math.PI / 2.0f);
            Sprites[3].SpriteEffect = SpriteEffects.FlipHorizontally;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Display)
            {
                frameSprites[0].Draw(spriteBatch);
                frameSprites[1].Draw(spriteBatch);
                frameSprites[2].Draw(spriteBatch);
                frameSprites[3].Draw(spriteBatch);
                frameSprites[4].Draw(spriteBatch);
                frameSprites[5].Draw(spriteBatch);
                frameSprites[6].Draw(spriteBatch);
                frameSprites[7].Draw(spriteBatch);
                Sprites[0].Draw(spriteBatch);
                Sprites[1].Draw(spriteBatch);
                Sprites[2].Draw(spriteBatch);
                Sprites[3].Draw(spriteBatch);

                fontGraphics.Draw(spriteBatch);
            }
        }
    }
}

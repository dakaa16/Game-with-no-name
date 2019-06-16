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
    public class Area
    {
        public string backGroundName;
        private Texture2D backGroundTexture;
        private Sprite backGroundSprite;
        public Vector2 Size;
        public List<Block> Blocks;
        public List<Building> Buildings;
        public List<CollisionRect> CollisionRects;
        public List<CollisionRect> AlphaRects;
        public List<TransitionRect> TransitionRects;
        public List<Texture2D> Textures;
        public List<string> TextureNames;

        public Area ()
        {
            Blocks = new List<Block>();
            Buildings = new List<Building>();
            CollisionRects = new List<CollisionRect>();
            AlphaRects = new List<CollisionRect>();
            TransitionRects = new List<TransitionRect>();
            Textures = new List<Texture2D>();
            TextureNames = new List<string>();
        }

        public Sprite getBackGroundSprite()
        {
            return backGroundSprite;
        }

        public void Load (ContentManager content)
        {
            content.RootDirectory = "Content";
            backGroundTexture = content.Load<Texture2D>(backGroundName);
            foreach (string name in TextureNames)
            {
                Textures.Add(content.Load<Texture2D>(name));
            }
        }

        public void Build ()
        {
            backGroundSprite = new Sprite(backGroundTexture);
            backGroundSprite.ZDepth = 0.0000001f;
            foreach (Block block in Blocks)
            {
                block.Build(Textures);
                CollisionRects.Add(block.getColRect());
            }
            foreach (Building building in Buildings)
            {
                building.Build(Textures);
                CollisionRects.Add(building.getColRect());
                AlphaRects.Add(building.getAlphaRect());
                TransitionRects.Add(building.transRect);
            }
        }

        public void Dispose()
        {
            backGroundName = "";
            backGroundTexture = null;
            backGroundSprite = null;
            Size = Vector2.Zero;
            Blocks.Clear();
            Buildings.Clear();
            CollisionRects.Clear();
            AlphaRects.Clear();
            TransitionRects.Clear();
            Textures.Clear();
            TextureNames.Clear();
    }

        public void UnLoad (ContentManager content)
        {
            content.RootDirectory = "Content";
            foreach (Texture2D tex in Textures)
            {
                //Implement
            }
        }

        public void Translate (Vector2 velocity /*CollisionRect cR, float speed*/)
        {
            backGroundSprite.Position += velocity;
            foreach (Block block in Blocks)
            {
                block.Translate(velocity, false);
            }
            foreach (Building building in Buildings)
            {
                building.Translate(velocity, false);
            }
            foreach (CollisionRect cR in CollisionRects)
            {
                cR.Position += velocity;
            }
            foreach (CollisionRect aRect in AlphaRects)
            {
                aRect.Position += velocity;
            }
            foreach (TransitionRect tRec in TransitionRects)
            {
                tRec.Position += velocity;
            }
        }


        public TransitionRect TransitionIntersection(CollisionRect colRect)
        {
            foreach (TransitionRect tR in TransitionRects)
            {
                if (tR.Rect.Intersects(colRect.Rect))
                {
                    return tR;
                }
            }
            return null;
        }

        public void Update (CollisionRect colRect)
        {
            foreach (Block block in Blocks)
            {
                block.Update();
            }
            foreach (Building building in Buildings)
            {
                building.Update();
                building.AlphaIntersect(colRect);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            backGroundSprite.Draw(spriteBatch);
            foreach (Block block in Blocks)
            {
                block.Draw(spriteBatch);
            }
            foreach (Building building in Buildings)
            {
                building.Draw(spriteBatch);
            }
        }

        //public void DrawDebug(SpriteBatch spriteBatch)
        //{
        //    foreach (CollisionRect cR in CollisionRects)
        //    {
        //        spriteBatch.Draw()
        //    }
        //    foreach (Building building in Buildings)
        //    {
        //        building.Draw(spriteBatch);
        //    }
        //}
    }
}

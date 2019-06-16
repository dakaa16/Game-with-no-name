using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLData
{
    public class Building : Block
    {
        CollisionRect colRect;
        CollisionRect alphaRect;
        public TransitionRect transRect;
        public int BottomCollideHeight;
        AnimatedSprite aniSprite;
        private bool alphaAnimate;
        private float alphaAnimateGoal;

        public void Build(List<Texture2D> Textures)
        {
            aniSprite = new AnimatedSprite(Textures[textureIndex], this.getPosition(), rows, columns, index);
            aniSprite.ZDepth = ((this.getPosition().Y + this.getDimension().Y) / 10000) + 0.01f;
            alphaRect = new CollisionRect();
            alphaRect.Rect = new Rectangle((int)getPosition().X, (int)getPosition().Y, (int)getDimension().X, (int)getDimension().Y - BottomCollideHeight);
            colRect = new CollisionRect();
            colRect.Rect = new Rectangle((int)getPosition().X, (int)getPosition().Y + alphaRect.Rect.Height, (int)getDimension().X, BottomCollideHeight);
            colRect.LeftExposed = leftExposed;
            colRect.RightExposed = rightExposed;
            colRect.TopExposed = topExposed;
            colRect.BottomExposed = bottomExposed;
            colRect.setBuilding(this);
        }

        public CollisionRect getColRect()
        {
            return colRect;
        }

        public CollisionRect getAlphaRect()
        {
            return alphaRect;
        }

        public void Translate(Vector2 velocity, bool translateCollisionRect)
        {
            aniSprite.Position += velocity;
            if (translateCollisionRect)
                colRect.Position += velocity;
        }

        public void AlphaIntersect(CollisionRect cR)
        {
            if (!alphaRect.Rect.Intersects(cR.Rect))
            {
                alphaAnimateGoal = 1.0f;
                alphaAnimate = true;
            }
            else
            {
                alphaAnimate = true;
                alphaAnimateGoal = 0.5f;
            }
        }

        public static bool AlmostEqual(float a, float b)

        {

            if (a == b)

            {

                return true;

            }

            return Math.Abs(a - b) < Math.Abs(a) / 281474976710656.0;

        }

        public void Update()
        {
            if (alphaAnimate)
            {
                if (aniSprite.Alpha > alphaAnimateGoal)
                {
                    if (AlmostEqual(aniSprite.Alpha, alphaAnimateGoal))
                    {
                        aniSprite.Alpha = alphaAnimateGoal;
                        alphaAnimate = false;
                    }
                    else
                        aniSprite.Alpha -= 0.025f;
                }
                else
                {
                    if (AlmostEqual(aniSprite.Alpha, alphaAnimateGoal))
                    {
                        aniSprite.Alpha = alphaAnimateGoal;
                        alphaAnimate = false;
                    }
                    else
                        aniSprite.Alpha += 0.025f;
                }
            }
            if (animate)
            {
                aniSprite.Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            aniSprite.Draw(spriteBatch);
        }
    }
}

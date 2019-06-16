using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XMLData;

namespace Game1
{
    class CollisionCheck
    {
        Player player;
        public CollisionRect colRect;
        public CollisionCheck(Player player)
        {
            this.player = player;
            colRect = player.ColRect;
            colRect.RightExposed = true;
            colRect.LeftExposed = true;
            colRect.TopExposed = true;
            colRect.BottomExposed = true;
        }

        protected bool IsTouchingLeft(CollisionRect oRect, Vector2 velocity)
        {
            return colRect.Rect.Right >= oRect.Rect.Left &&
              colRect.Rect.Left <= oRect.Rect.Left &&
              colRect.Rect.Bottom >= oRect.Rect.Top &&
              colRect.Rect.Top <= oRect.Rect.Bottom;
        }

        protected bool IsTouchingRight(CollisionRect oRect, Vector2 velocity)
        {
            return colRect.Rect.Left <= oRect.Rect.Right &&
              colRect.Rect.Right >= oRect.Rect.Right &&
              colRect.Rect.Bottom >= oRect.Rect.Top &&
              colRect.Rect.Top <= oRect.Rect.Bottom;
        }

        protected bool IsTouchingTop(CollisionRect oRect, Vector2 velocity)
        {
            return colRect.Rect.Bottom >= oRect.Rect.Top &&
              colRect.Rect.Top <= oRect.Rect.Top &&
              colRect.Rect.Right >= oRect.Rect.Left &&
              colRect.Rect.Left <= oRect.Rect.Right;
        }

        protected bool IsTouchingBottom(CollisionRect oRect, Vector2 velocity)
        {
            return colRect.Rect.Top <= oRect.Rect.Bottom &&
              colRect.Rect.Bottom >= oRect.Rect.Bottom &&
              colRect.Rect.Right >= oRect.Rect.Left &&
              colRect.Rect.Left <= oRect.Rect.Right;
        }

        public Vector2 Move(Area area, Vector2 velocity)
        {
            float xCorrection = 0;
            float yCorrection = 0;
            colRect.Position = player.Position + velocity;
            for (int i = 0; i < area.CollisionRects.Count; i++)
            {
                int xTempVelocity = 0;
                int yTempVelocity = 0;
                CollisionRect cR;
                cR = area.CollisionRects[i];

                if (colRect.Rect.Intersects(cR.Rect))
                {
                    //System.Diagnostics.Debug.WriteLine("Intersection: " + cR.Position);
                    if (velocity.X > 0 && IsTouchingLeft(cR, velocity))
                    {
                        xTempVelocity = cR.rightCorrection(colRect, velocity);
                        if (xTempVelocity < xCorrection)
                        {
                            xCorrection = xTempVelocity;
                        }
                    }
                    else if (velocity.X < 0 && IsTouchingRight(cR, velocity))
                    {
                        xTempVelocity = cR.leftCorrection(colRect, velocity);
                        if (xTempVelocity > xCorrection)
                        {
                            xCorrection = xTempVelocity;
                        }
                    }

                    if (velocity.Y < 0 && IsTouchingBottom(cR, velocity))
                    {
                        yTempVelocity = cR.topCorrection(colRect, velocity);
                        if (yTempVelocity > yCorrection)
                        {
                            yCorrection = yTempVelocity;
                        }
                    }
                    else if (velocity.Y > 0 && IsTouchingTop(cR, velocity))
                    {
                        yTempVelocity = cR.bottomCorrection(colRect, velocity);
                        if (yTempVelocity < yCorrection)
                        {
                            yCorrection = yTempVelocity;
                        }
                    }
                }   
            }
            velocity = new Vector2(velocity.X + xCorrection, velocity.Y + yCorrection);
            return velocity;
        }
    }
}

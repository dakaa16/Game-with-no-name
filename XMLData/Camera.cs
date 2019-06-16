using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLData;

namespace Game1
{
    public class Camera
    {
        public Vector2 OffSet = Vector2.Zero;
        bool panning;

        public Vector2 getCurrentPosition(Area area)
        {
            return new Vector2(area.getBackGroundSprite().Position.X * -1, area.getBackGroundSprite().Position.Y * -1);
        }

        public Vector2 CenterPosition(Vector2 position, Area area, int screenWidth, int screenHeight)
        {
            if (OffSet != Vector2.Zero)
            {
                return Vector2.Zero;
            }
            return new Vector2(getCurrentPosition(area).X - (position.X - (screenWidth / 2)), getCurrentPosition(area).Y - (position.Y - (screenHeight / 2)));
        }

        public bool isPanning(Player player, Area area, int screenWidth, int screenHeight, float speed)
        {
            return (MoveRight(player, area, screenWidth, speed) || MoveLeft(player, area, screenWidth, speed) || MoveUp(player, area, screenHeight, speed) || MoveDown(player, area, screenHeight, speed));
        }

        public bool MoveRight(Player player, Area area, int screenWidth, float speed)
        {
            return area.getBackGroundSprite().Position.X <= 0 && player.Position.X > screenWidth/2 - speed /* + screenWidth/8*/;
        }
        public bool MoveLeft(Player player, Area area, int screenWidth, float speed)
        {
            return area.getBackGroundSprite().Position.X + area.getBackGroundSprite().Texture.Width >= screenWidth && player.Position.X < screenWidth / 2 + speed /*- screenWidth / 8*/;
        }
        public bool MoveUp(Player player, Area area, int screenHeight, float speed)
        {
            return area.getBackGroundSprite().Position.Y <= 0 && player.Position.Y < (screenHeight / 2 + speed /*- screenHeight / 8*/);
        }
        public bool MoveDown(Player player, Area area, int screenHeight, float speed)
        {
            return area.getBackGroundSprite().Position.Y + area.getBackGroundSprite().Texture.Height >= screenHeight && player.Position.Y > screenHeight / 2 - speed /*+ screenHeight / 8*/;
        }

        public Vector2 RightCorrection(Area area, int screenWidth)
        {
            Vector2 velocity = Vector2.Zero;
            if (area.getBackGroundSprite().Position.X + area.getBackGroundSprite().Texture.Width < screenWidth)
            {
                velocity.X = screenWidth - (area.getBackGroundSprite().Position.X + area.getBackGroundSprite().Texture.Width);
            }
            return velocity;
        }
        public Vector2 LeftCorrection (Area area, int screenWidth)
        {
            Vector2 velocity = Vector2.Zero;
            if (area.getBackGroundSprite().Position.X > 0)
            {
                velocity.X = -area.getBackGroundSprite().Position.X;
            }
            return velocity;
        }

        public Vector2 UpCorrection(Area area, int screenHeight)
        {
            Vector2 velocity = Vector2.Zero;
            if (area.getBackGroundSprite().Position.Y > 0)
            {
                velocity.Y = -area.getBackGroundSprite().Position.Y;
            }
            return velocity;
        }

        public Vector2 DownCorrection(Area area, int screenHeight)
        {
            Vector2 velocity = Vector2.Zero;
            if (area.getBackGroundSprite().Position.Y + area.getBackGroundSprite().Texture.Height < screenHeight)
            {
                velocity.Y = screenHeight - (area.getBackGroundSprite().Position.Y + area.getBackGroundSprite().Texture.Height);
            }
            return velocity;
        }

        public Vector2 Correction(Area area, int screenWidth, int screenHeight)
        {
            Vector2 velocity = Vector2.Zero;
            velocity += RightCorrection(area, screenWidth);
            velocity += LeftCorrection(area, screenWidth);
            velocity += UpCorrection(area, screenHeight);
            velocity += DownCorrection(area, screenHeight);
            return velocity;
        }

        public void CalculatePotentialOffset(Area area, int screenWidth, int screenHeight)
        {
            OffSet = Vector2.Zero;
            if (area.Size.X < screenWidth)
            {
                OffSet = new Vector2((screenWidth - area.Size.X) / 2, OffSet.Y);
            }
            if (area.Size.Y < screenHeight)
            {
                OffSet = new Vector2(OffSet.X, (screenHeight - area.Size.Y) / 2);
            }
        }

        public void Move(Vector2 velocity,Area area, Player player, float playerSpeed, int screenWidth, int screenHeight)
        {
            if (OffSet.X != 0)
            {
                player.Position += new Vector2(velocity.X, 0);
            }
            else
            {
                if (velocity.X > 0)
                {
                    if (MoveRight(player, area, screenWidth, playerSpeed))
                    {
                        area.Translate(-1 * new Vector2(velocity.X, 0));
                        Vector2 correction = RightCorrection(area, screenWidth);
                        if (correction != Vector2.Zero)
                        {
                            area.Translate(correction);
                            player.Position += correction;
                        }
                    }
                    else
                    {
                        player.Position += new Vector2(velocity.X, 0);
                    }
                }
                else if (velocity.X < 0)
                {
                    if (MoveLeft(player, area, screenWidth, playerSpeed))
                    {
                        area.Translate(-1 * new Vector2(velocity.X, 0));
                        Vector2 correction = LeftCorrection(area, screenWidth);
                        if (correction != Vector2.Zero)
                        {
                            area.Translate(correction);
                            player.Position += correction;
                        }
                    }
                    else
                    {
                        player.Position += new Vector2(velocity.X, 0);
                    }
                }
            }

            if (OffSet.Y != 0)
            {
                player.Position += new Vector2(0, velocity.Y);
            }
            else
            {
                if (velocity.Y < 0)
                {
                    if (MoveUp(player, area, screenHeight, playerSpeed))
                    {
                        area.Translate(-1 * new Vector2(0, velocity.Y));
                        Vector2 correction = UpCorrection(area, screenHeight);
                        if (correction != Vector2.Zero)
                        {
                            area.Translate(correction);
                            player.Position += correction;
                        }
                    }
                    else
                    {
                        player.Position += new Vector2(0, velocity.Y);
                    }
                }
                else if (velocity.Y > 0)
                {
                    if (MoveDown(player, area, screenHeight, playerSpeed))
                    {
                        area.Translate(-1 * new Vector2(0, velocity.Y));
                        Vector2 correction = DownCorrection(area, screenHeight);
                        if (correction != Vector2.Zero)
                        {
                            area.Translate(correction);
                            player.Position += correction;
                        }
                    }
                    else
                    {
                        player.Position += new Vector2(0, velocity.Y);
                    }
                }
            }
        }

    }
}

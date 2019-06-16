using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLData;

namespace Game1
{
    public enum PlayerFacing { Right, Left, Up, Down, UpRight, DownRight, UpLeft, DownLeft}
    public class Player
    {
        PlayerFacing playerFacing;
        public PlayerFacing Facing { get { return playerFacing; } }
        Sprite playerSprite;
        public Sprite PlayerSprite { get { return playerSprite; } }
        CollisionRect colRect;
        public CollisionRect ColRect { get { return colRect; } }
        public Vector2 Position {get; set;}
        public float ZDepth { get; set; }
        public float playerSpeed { get; set; }
        float lastPlayerSpeed;
        float totalPlayerSpeed;
        float currentSpeed;
        public Player(Sprite sprite, float playerSpeed)
        {
            playerFacing = PlayerFacing.Down;
            playerSprite = sprite;
            colRect = new CollisionRect(sprite);
            this.playerSpeed = playerSpeed;
            totalPlayerSpeed = playerSpeed;
        }

        public int getCurrentSpeed()
        {
            currentSpeed = totalPlayerSpeed - lastPlayerSpeed;
            totalPlayerSpeed -= lastPlayerSpeed;
            totalPlayerSpeed += playerSpeed;
            lastPlayerSpeed = (int)currentSpeed;
            return (int)currentSpeed;
        }

        private void setPlayerFacing(Vector2 velocity)
        {
            if (velocity.X == 0 && velocity.Y == 0)
            {

            }
            else if (velocity.X != 0 && velocity.Y != 0)
            {
                if (velocity.X > 0 && velocity.Y < 0)
                {
                    playerFacing = PlayerFacing.UpRight;
                }
                else if(velocity.X > 0 && velocity.Y > 0)
                {
                    playerFacing = PlayerFacing.DownRight;
                }
                else if(velocity.X < 0 && velocity.Y < 0)
                {
                    playerFacing = PlayerFacing.UpLeft;
                }
                else if(velocity.X < 0 && velocity.Y > 0)
                {
                    playerFacing = PlayerFacing.DownLeft;
                }
            }
            else
            {
                if (velocity.X > 0)
                {
                    playerFacing = PlayerFacing.Right;
                }
                else if(velocity.X < 0)
                {
                    playerFacing = PlayerFacing.Left;
                }
                else if(velocity.Y < 0)
                {
                    playerFacing = PlayerFacing.Up;
                }
                else if(velocity.Y > 0)
                {
                    playerFacing = PlayerFacing.Down;
                }
            }
        }

        public void Update(Vector2 velocity)
        {
            setPlayerFacing(velocity);
            playerSprite.Position = Position;
            playerSprite.ZDepth = ZDepth;
            colRect.Position = Position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            playerSprite.Draw(spriteBatch);
        }
    }
}

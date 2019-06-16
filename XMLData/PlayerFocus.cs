using Game1;
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
    public class PlayerFocus
    {
        Vector2 position;
        public Vector2 Position { get { return position; } }
        Sprite focusSprite;
        public PlayerFocus(ContentManager Content)
        {
            focusSprite = new Sprite(Content.Load<Texture2D>("playerFocus"));
        }

        public void SetFocus(Camera camera, Area area, Player player)
        {
            int posX = 0;
            int posY = 0;
            if (player.Facing == PlayerFacing.Up)
            {
                posX = (int)((player.Position.X + camera.getCurrentPosition(area).X + (player.ColRect.Rect.Width / 2)) / Global.TileSize);
                posY = (int)((player.Position.Y + camera.getCurrentPosition(area).Y - 1) / Global.TileSize);
            }
            else if (player.Facing == PlayerFacing.Down)
            {
                posX = (int)((player.Position.X + camera.getCurrentPosition(area).X + (player.ColRect.Rect.Width / 2)) / Global.TileSize);
                posY = (int)((player.Position.Y + camera.getCurrentPosition(area).Y + player.ColRect.Rect.Height + 1) / Global.TileSize);
            }
            else if (player.Facing == PlayerFacing.Right || player.Facing == PlayerFacing.UpRight || player.Facing == PlayerFacing.DownRight)
            {
                posX = (int)((player.Position.X + camera.getCurrentPosition(area).X + player.ColRect.Rect.Width + 1) / Global.TileSize);
                posY = (int)((player.Position.Y + camera.getCurrentPosition(area).Y + (player.ColRect.Rect.Height / 2)) / Global.TileSize);
            }
            else if (player.Facing == PlayerFacing.Left || player.Facing == PlayerFacing.UpLeft || player.Facing == PlayerFacing.DownLeft)
            {
                posX = (int)((player.Position.X + camera.getCurrentPosition(area).X - 1) / Global.TileSize);
                posY = (int)((player.Position.Y + camera.getCurrentPosition(area).Y + (player.ColRect.Rect.Height / 2)) / Global.TileSize);
            }
            Vector2 positionVector = new Vector2((posX * Global.TileSize) - camera.getCurrentPosition(area).X, (posY * Global.TileSize) - camera.getCurrentPosition(area).Y);
            position = positionVector;
            focusSprite.Position = positionVector;
            //System.Diagnostics.Debug.WriteLine(positionVector);
        }

        public CollisionRect getFocus(Area area)
        {
            foreach (CollisionRect cR in area.CollisionRects)
            {
                if (position == cR.Position)
                {
                    return cR;
                    //if (cR.getBlock() != null)
                    //{
                    //    interactionBox.DisplayBox(GraphicsDevice, cR.getBlock().interactions, cR.Position, cR.getBlock().checkText);
                    //}
                    //if (cR.getBuilding() != null)
                    //{
                    //    interactionBox.DisplayBox(GraphicsDevice, cR.getBuilding().interactions, cR.Position, cR.getBuilding().checkText);
                    //}
                    //break;
                }
            }
            return null;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            focusSprite.Draw(spriteBatch);
        }
    }
}

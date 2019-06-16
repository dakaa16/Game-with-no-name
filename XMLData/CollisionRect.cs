using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace XMLData
{
    public class CollisionRect
    {
        private Rectangle rect;
        private bool leftExposed;
        private bool rightExposed;
        private bool topExposed;
        private bool bottomExposed;
        private Block blockReference;
        private Building buildingReference;


        public CollisionRect()
        {

        }
        public CollisionRect(Sprite sprite)
        {
            rect = new Rectangle((int)sprite.Position.X, (int)sprite.Position.Y, sprite.DrawRec.Width, sprite.DrawRec.Height);
            leftExposed = true;
            rightExposed = true;
            topExposed = true;
            bottomExposed = true;
        }
        public CollisionRect(AnimatedSprite sprite)
        {
            rect = new Rectangle((int)sprite.Position.X, (int)sprite.Position.Y, sprite.DrawRec.Width, sprite.DrawRec.Height);
            leftExposed = true;
            rightExposed = true;
            topExposed = true;
            bottomExposed = true;
        }

        public void setBlock(Block block)
        {
            blockReference = block;
        }
        public Block getBlock()
        {
            return blockReference;
        }
        public void setBuilding(Building building)
        {
            buildingReference = building;
        }
        public Building getBuilding()
        {
            return buildingReference;
        }

        public bool LeftExposed
        {
            get { return leftExposed; }
            set { leftExposed = value; }
        }
        public bool RightExposed
        {
            get { return rightExposed; }
            set { rightExposed = value; }
        }
        public bool TopExposed
        {
            get { return topExposed; }
            set { topExposed = value; }
        }
        public bool BottomExposed
        {
            get { return bottomExposed; }
            set { bottomExposed = value; }
        }

        public Rectangle Rect
        {
            get
            {
                return rect;
            }
            set
            {
                rect = value;
            }
        }

        public Vector2 Position
        {
            get
            {
                return new Vector2(rect.Location.X, rect.Location.Y);
            }
            set
            {
                rect = new Rectangle((int)value.X, (int)value.Y, rect.Width, rect.Height);
            }
        }

        public Vector2 Dimensions
        {
            get
            {
                return new Vector2(rect.Width, rect.Height);
            }
            set
            {
                rect = new Rectangle(rect.Location.X, rect.Location.Y, (int)value.X, (int)value.Y);
            }
        }

        public int rightCorrection(CollisionRect colRect, Vector2 velocity)
        {
            if (leftExposed)
            {
                if (rect.Left - colRect.Rect.Right < -velocity.X)
                    return 0;
                return rect.Left - colRect.Rect.Right;
            }
            return 0;
            //if (leftExposed)
            //{
            //    System.Diagnostics.Debug.WriteLine(colRect.Rect.Right - rect.Left);
            //    if (colRect.Rect.Right - rect.Left < -velocity.X)
            //        return 0;
            //    return colRect.Rect.Right - rect.Left;
            //}
            //return 0;
        }
        public int leftCorrection(CollisionRect colRect, Vector2 velocity)
        {
            if (rightExposed)
            {
                if (rect.Right - colRect.Rect.Left > -velocity.X)
                    return 0;
                return rect.Right - colRect.Rect.Left;
            }
            return 0;
            //if (rightExposed)
            //{
            //    System.Diagnostics.Debug.WriteLine(colRect.Rect.Right - rect.Left);
            //    if (colRect.Rect.Right - rect.Left > -velocity.X)
            //        return 0;
            //    return colRect.Rect.Right - rect.Left;
            //}
            //return 0;
        }
        public int topCorrection(CollisionRect colRect, Vector2 velocity)
        {
            if (bottomExposed)
            {
                if (rect.Bottom - colRect.Rect.Top > -velocity.Y)
                    return 0;
                return rect.Bottom - colRect.Rect.Top;
            }
            return 0;
        }
        public int bottomCorrection(CollisionRect colRect, Vector2 velocity)
        {
            if (topExposed)
            {
                if (rect.Top - colRect.Rect.Bottom < -velocity.Y)
                {
                    return 0;
                }
                return rect.Top - colRect.Rect.Bottom;
            }
            return 0;
        }

    }
}

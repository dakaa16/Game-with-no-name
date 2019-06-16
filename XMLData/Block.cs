using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XMLData
{
    public class Block
    {
        CollisionRect colRect;
        AnimatedSprite aniSprite;
        public bool leftExposed;
        public bool rightExposed;
        public bool topExposed;
        public bool bottomExposed;
        public Vector2 tilePosition;
        public Vector2 tileDimensions;
        public int index;
        public int textureIndex;
        public int rows;
        public int columns;
        public bool animate;
        public string checkText;
        public InteractionBox.Interaction[] interactions;

        public Block ()
        {
            
        }

        public Block(List<CollisionRect> colRectList, List<Texture2D> aniSpriteTextureList)
        {
            aniSprite.Texture = aniSpriteTextureList[textureIndex];
            if (!(leftExposed && rightExposed && topExposed && bottomExposed))
            {
                colRect = new CollisionRect(aniSprite);
                colRect.LeftExposed = leftExposed;
                colRect.RightExposed = rightExposed;
                colRect.TopExposed = topExposed;
                colRect.BottomExposed = bottomExposed;
                colRectList.Add(colRect);
            }
        }

        public Block(List<CollisionRect> colRectList, List<Texture2D> aniSpriteTextureList, int textureIndex, int rows, int columns, int index, bool animate, Vector2 tilePosition)
        {
            this.tilePosition = tilePosition;
            aniSprite = new AnimatedSprite(aniSpriteTextureList[textureIndex], this.getPosition(), rows, columns);
            this.index = index;
            this.animate = animate;
            colRect = new CollisionRect(aniSprite);
            colRectList.Add(colRect);
            this.tileDimensions = colRect.Dimensions / Global.TileSize;
        }

        public Vector2 getPosition()
        {
            return new Vector2(tilePosition.X * Global.TileSize, tilePosition.Y * Global.TileSize);
        }
        public Vector2 getDimension()
        {
            return new Vector2(tileDimensions.X * Global.TileSize, tileDimensions.Y * Global.TileSize);
        }
        public void Build(List<Texture2D> Textures)
        {
            aniSprite = new AnimatedSprite(Textures[textureIndex], this.getPosition(), rows, columns, index);
            aniSprite.ZDepth = ((this.getPosition().Y + this.getDimension().Y) / 10000) + 0.01f;
            colRect = new CollisionRect(aniSprite);
            colRect.LeftExposed = leftExposed;
            colRect.RightExposed = rightExposed;
            colRect.TopExposed = topExposed;
            colRect.BottomExposed = bottomExposed;
            colRect.setBlock(this);
        }

        public CollisionRect getColRect()
        {
            return colRect;
        }

        protected bool IsTouchingLeft(CollisionRect oRect, Vector2 velocity)
        {
            return colRect.Rect.Right - velocity.X >= oRect.Rect.Left &&
              colRect.Rect.Left <= oRect.Rect.Left &&
              colRect.Rect.Bottom >= oRect.Rect.Top &&
              colRect.Rect.Top <= oRect.Rect.Bottom;
        }

        protected bool IsTouchingRight(CollisionRect oRect, Vector2 velocity)
        {
            return colRect.Rect.Left + velocity.X <= oRect.Rect.Right &&
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

        public void Translate (Vector2 velocity, bool translateCollisionRect)
        {
            aniSprite.Position += velocity;
            if (translateCollisionRect)
                colRect.Position += velocity;
        }

        public void Update()
        {
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

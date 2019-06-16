using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace XMLData
{
    public class AnimatedSprite
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle DrawRec { get; set; }
        public float Alpha { get; set; }
        public float Rotation { get; set; }
        public Vector2 Origin { get; set; }
        public float Scale { get; set; }
        public SpriteEffects SpriteEffect { get; set; }
        public float ZDepth { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;

        public AnimatedSprite()
        {
            
        }

        public AnimatedSprite(Texture2D texture, Vector2 position, int rows, int columns)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            Position = position;
            DrawRec = new Rectangle(0, 0, Texture.Width / columns, Texture.Height / rows);
            Alpha = 1.0f;
            Rotation = 0.0f;
            Origin = new Vector2(0, 0);
            Scale = 1.0f;
            SpriteEffect = SpriteEffects.None;
            ZDepth = 0.1f;
        }

        public AnimatedSprite(Texture2D texture, Vector2 position, int rows, int columns, int currentFrame)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            this.currentFrame = currentFrame;
            totalFrames = Rows * Columns;
            Position = position;
            DrawRec = new Rectangle(0, 0, Texture.Width / columns, Texture.Height / rows);
            Alpha = 1.0f;
            Rotation = 0.0f;
            Origin = new Vector2(0, 0);
            Scale = 1.0f;
            SpriteEffect = SpriteEffects.None;
            ZDepth = 0.1f;
        }

        public void Update()
        {
            currentFrame++;
            if (currentFrame == totalFrames)
                currentFrame = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, width, height);

            //spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White * Alpha, Rotation, Origin, SpriteEffect, ZDepth);
            spriteBatch.Draw(Texture, Position, sourceRectangle, Color.White * Alpha, Rotation, Origin, Scale, SpriteEffect, ZDepth);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            //spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White * Alpha, Rotation, Origin, SpriteEffect, ZDepth);
            spriteBatch.Draw(Texture, Position, sourceRectangle, Color.White * Alpha, Rotation, Origin, Scale, SpriteEffect, ZDepth);
        }
    }
}

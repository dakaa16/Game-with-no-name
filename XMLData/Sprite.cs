using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XMLData
{
    public class Sprite
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

        public Sprite (Texture2D texture)
        {
            Texture = texture;
            Position = Vector2.Zero;
            DrawRec = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Alpha = 1.0f;
            Rotation = 0.0f;
            Origin = new Vector2(0, 0);
            Scale = 1.0f;
            SpriteEffect = SpriteEffects.None;
            ZDepth = 0.1f;
        }

        public Sprite(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
            DrawRec = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Alpha = 1.0f;
            Rotation = 0.0f;
            Origin = new Vector2(0, 0);
            Scale = 1.0f;
            SpriteEffect = SpriteEffects.None;
            ZDepth = 0.1f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, DrawRec, Color.White * Alpha, Rotation, Origin, Scale, SpriteEffect, ZDepth);
        }
    }
}

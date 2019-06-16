using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLData
{
    public class FontGraphics
    {
        public SpriteFont Font { get; set; }
        public string Text { get; set; }
        public Color color { get; set; }
        public Vector2 Position { get; set; }
        public int PaddingX;
        public int PaddingY;
        private Vector2 dimensions;
        private Color[] data;
        public Texture2D texture { get; set; }
        public FontGraphics(GraphicsDevice graphics, SpriteFont font, string text, Color color, Vector2 position)
        {
            Font = font;
            Text = text;
            this.color = color;
            Position = position;
            PaddingX = 0;
            PaddingY = 0;
            dimensions = Font.MeasureString(Text);
            data = new Color[(int)dimensions.X * (int)dimensions.Y];
            texture = new Texture2D(graphics, (int)dimensions.X, (int)dimensions.Y);
            for (int i = 0; i < data.Length; ++i)
                data[i] = color;

            texture.SetData(data);
        }
        public FontGraphics(GraphicsDevice graphics, SpriteFont font, string text, Color color, Vector2 position, int paddingX, int paddingY)
        {
            Font = font;
            Text = text;
            this.color = color;
            Position = position;
            PaddingX = paddingX;
            PaddingY = paddingY;
            dimensions = new Vector2(Font.MeasureString(Text).X + PaddingX, Font.MeasureString(Text).Y + PaddingY);
            data = new Color[(int)dimensions.X * (int)dimensions.Y];
            texture = new Texture2D(graphics, (int)dimensions.X, (int)dimensions.Y);
            for (int i = 0; i < data.Length; ++i)
                data[i] = color;

            texture.SetData(data);
        }

        public void Update(GraphicsDevice graphics)
        {
            if (!Text.Equals(""))
            {
                dimensions = new Vector2(Font.MeasureString(Text).X + PaddingX, Font.MeasureString(Text).Y + PaddingY);
                data = new Color[(int)dimensions.X * (int)dimensions.Y];
                texture = new Texture2D(graphics, (int)dimensions.X, (int)dimensions.Y);
                for (int i = 0; i < data.Length; ++i)
                    data[i] = color;

                texture.SetData(data);
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
            spriteBatch.DrawString(Font, Text, new Vector2(Position.X + (PaddingX / 2), Position.Y + (PaddingY / 2)), Color.White);
        }
    }
}

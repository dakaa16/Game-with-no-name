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
    public class InteractionBox
    {
        public enum Interaction { None, Check, PickUp, Talk, Shop}
        public Interaction[] interactions;
        public Vector2 Position { get; set; }
        public bool Display { get; set; }
        Vector2 oldPosition;
        string text;
        string oldText;
        InteractionBoxGraphic IBG;


        public InteractionBox(ContentManager Content, GraphicsDevice graphics, SpriteFont font)
        {
            IBG = new InteractionBoxGraphic(Content, graphics, font);
            interactions = new Interaction[10];
        }

        public void DoInteraction(Interaction interaction, object data)
        {
            if (interaction == Interaction.None)
            {
                //do nothing
            }
            else if (interaction == Interaction.Check)
            {
                string text = (string)data;
                System.Diagnostics.Debug.WriteLine(text);
                //display text
            }
            else if (interaction == Interaction.PickUp)
            {
                //Item item = (Item)data;
                //Remove item from area
                //Player obtain item
            }
            else if (interaction == Interaction.Talk)
            {
                //Character character = (Character)data;
                //display text
                //display character name
                //draw character detailed sprite
            }
            else if (interaction == Interaction.Shop)
            {
                //Shop shop = (Shop)data;
                //display shop Gui
            }
        }

        private string createString()
        {
            string t = "";
            foreach (Interaction i in interactions)
            {
                if (i == Interaction.None)
                {
                    t += "None\n\n";
                }
                else if (i == Interaction.Check)
                {
                    t += "Check\n\n";
                }
                else if (i == Interaction.PickUp)
                {
                    t += "Pick Up\n\n";
                }
                else if (i == Interaction.Talk)
                {
                    t += "Talk\n\n";
                }
                else if (i == Interaction.Shop)
                {
                    t += "Shop\n\n";
                }
            }
            t = t.Substring(0, t.Length - 2);
            return t;
        }

        public void DisplayBox(GraphicsDevice graphics, Interaction[] interactions, Vector2 position, string tempCheckText)
        {
            this.interactions = interactions;
            Position = position;
            if (interactions.Length > 1)
            {
                Display = true;
                Update(graphics);
            }
            else
            {
                DoInteraction(interactions[0], tempCheckText);
            }
        }

        public void Update(GraphicsDevice graphics)
        {
            text = createString();
            IBG.Display = Display;
            if (!text.Equals(oldText) && Position != oldPosition)
            {
                oldText = text;
                oldPosition = Position;
                IBG.Text = text;
                IBG.Position = Position;
                IBG.Update(graphics);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            IBG.Draw(spriteBatch);
        }

    }
}

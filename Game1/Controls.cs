using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Controls
    {
        InputHelper inputHelper;

        public Controls()
        {
            inputHelper = new InputHelper();
        }

        public Vector2 getVelocityWASD()
        {
            Vector2 velocity = new Vector2(0, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                velocity = new Vector2(velocity.X, -1);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                velocity = new Vector2(velocity.X, 1);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                velocity = new Vector2(-1, velocity.Y);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                velocity = new Vector2(1, velocity.Y);
            }
            return velocity;
        }

        public bool ActionButtonPressed()
        {
            return (inputHelper.IsNewKeyPress(Keys.Space));
        }

        public bool diagonalVelocity(Vector2 velocity)
        {
            return velocity.X != 0 && velocity.Y != 0;
        }

        public Vector2 RoundVelocityToFastestInt(Vector2 velocity)
        {
            int tempX = 0;
            int tempY = 0;
            if (velocity.X > 0)
            {
                tempX = (int)Math.Ceiling(velocity.X);
            }
            else if (velocity.X < 0)
            {
                tempX = (int)Math.Floor(velocity.X);
            }
            if (velocity.Y > 0)
            {
                tempY = (int)Math.Ceiling(velocity.Y);
            }
            else if (velocity.Y < 0)
            {
                tempY = (int)Math.Floor(velocity.Y);
            }
            return new Vector2(tempX, tempY);
        }

        public void Update()
        {
            inputHelper.Update();
        }
    }
}

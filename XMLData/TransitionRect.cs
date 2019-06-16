using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLData
{
    public class TransitionRect : CollisionRect
    {
        public enum SpawnPosition { Above, Below, Left, Right };
        public string AreaName;
        public string ID;
        public SpawnPosition spawnPosition;

        public Area loadArea(ContentManager content)
        {
            content.RootDirectory = "Content";
            Area area = content.Load<Area>(AreaName);
            area.Load(content);
            area.Build();
            return area;
        }

        public Vector2 playerPosition(int Width, int Height)
        {
            if (spawnPosition == SpawnPosition.Above)
            {
                return new Vector2(Position.X + (Dimensions.X / 2) - (Width / 2), Position.Y - 1 - Height);
            }
            else if (spawnPosition == SpawnPosition.Below)
            {
                return new Vector2(Position.X + (Dimensions.X / 2) - (Width / 2), Position.Y + Dimensions.Y + 1);
            }
            return Vector2.Zero;
        }
    }
}

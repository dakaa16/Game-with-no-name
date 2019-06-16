using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Xml;
using XMLData;

namespace Game1
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {


            using (var game = new Game1())
                game.Run();
        }
    }
}

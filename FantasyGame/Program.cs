using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace FantasyGame
{
    class Program
    {
        static void Main(string[] args)
        {
            RenderWindow window = new RenderWindow(new VideoMode(1280, 720), "lol");
            View view = new View(new Vector2f(0, 0), new Vector2f(1280, 720));

            ContentManager.spriteMaps.Add(new SpriteMap(0, "LpYEB", "LpYEB.png", 16, 16));

            Map map = new Map("stuff.tmx");

            while(true)
            {
                window.Clear();

                map.Draw(window,view);

                window.Display();
            }


        }
    }
}

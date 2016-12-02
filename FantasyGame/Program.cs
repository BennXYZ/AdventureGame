﻿using System;
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
            window.SetFramerateLimit(60);
            View view = new View(new Vector2f(0, 0), new Vector2f(1280, 720));

            //testing stuff

            int lol = 0;
            ContentManager.spriteMaps.Add(new SpriteMap(0, "base_out_atlas", "base_out_atlas.png", 32, 32));
            ContentManager.spriteMaps.Add(new SpriteMap(0, "terrain_atlas", "terrain_atlas.png", 32, 32));
            ContentManager.spriteMaps.Add(new SpriteMap(0, "houses", "houses.png", 32, 32));

            Map map = new Map("fantasieWorld.tmx");

            Animation test = new Animation("terrain_atlas", 5, 120, 2);
            test.AnimationLoop = false;

            while(true)
            {
                //Map movement
                if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
                    view.Center = new Vector2f(view.Center.X, view.Center.Y + 10);
                if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
                    view.Center = new Vector2f(view.Center.X, view.Center.Y - 10);
                if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
                    view.Center = new Vector2f(view.Center.X - 10, view.Center.Y);
                if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
                    view.Center = new Vector2f(view.Center.X + 10, view.Center.Y);

                //Test Rectangles
                for (int i = 0; i < map.GetRectangles().Count; i++)
                    if (map.GetRectangles()[i].Intersects(new FloatRect(new Vector2f(view.Center.X, view.Center.Y), new Vector2f(5, 5))))
                    {
                        lol++;
                        Console.WriteLine(lol);
                    }

                test.Update();


                window.SetView(view);

                window.Clear();

                map.Draw(window,view);

                test.Draw(window);

                window.Display();

                if (test.EndOfAnimation())
                    window.Close();
            }


        }

        private void crazyshit(Vector2f position, List<FloatRect> collisions)
        {
            for (int i = 0; i < collisions.Count; i++)
                if (collisions[i].Intersects(new FloatRect(new Vector2f(position.X, position.Y), new Vector2f(5, 5))))
                    Console.WriteLine("lol");
        }
    }
}

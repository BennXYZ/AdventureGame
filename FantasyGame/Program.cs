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
            window.SetFramerateLimit(60);
            View view = new View(new Vector2f(0, 0), new Vector2f(1280, 720));

            //testing stuff

            int lol = 0;
            ContentManager.spriteMaps.Add(new SpriteMap(0, "base_out_atlas", "base_out_atlas.png", 32, 32));
            ContentManager.spriteMaps.Add(new SpriteMap(0, "terrain_atlas", "terrain_atlas.png", 32, 32));
            ContentManager.spriteMaps.Add(new SpriteMap(0, "houses", "houses.png", 32, 32));
            ContentManager.spriteMaps.Add(new SpriteMap(0, "player", "player.png", 23, 23));

            Map map = new Map("fantasieWorld.tmx");

            Player player = new Player("lol", 0, 10, new Vector2f(23, 23), new Vector2f(0, 0));
            //player.addAnimation(11, "player", 4, 60, 1);
            //player.addAnimation(12, "player", 4, 60, 2);
            //player.addAnimation(13, "player", 4, 60, 3);
            //player.addAnimation(14, "player", 4, 60, 4);

            while (true)
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

                player.Update(map.GetRectangles());

                window.SetView(view);

                window.Clear();


                map.Draw(window,view);
                player.Draw(window);

                window.Display();
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

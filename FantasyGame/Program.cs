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
            ContentManager.spriteMaps.Add(new SpriteMap(0, "collectables", "collectables.png", 28, 28));

            Map map = new Map("fantasieWorld.tmx");

            Player player = new Player("lol", 0, 10, new Vector2f(23, 23), new Vector2f(0, 0));
            GoldCoin gold = new GoldCoin(new Vector2f(10, 10));
            Thing thing = new Thing(new Vector2f(80, 80));

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

                if (player.Mask.Intersects(thing.mask))
                    thing.collect();

                window.SetView(view);

                window.Clear();


                map.Draw(window,view);
                player.Draw(window);
                gold.Draw(window);
                thing.Draw(window);

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

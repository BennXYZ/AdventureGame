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
            Inventory inventory = new Inventory();
            GoldCoin gold = new GoldCoin(new Vector2f(200, 200));
            Thing thing1 = new Thing(new Vector2f(80, 80));
            Thing thing2 = new Thing(new Vector2f(100, 80));
            Thing thing3 = new Thing(new Vector2f(150, 80));
            Thing thing4 = new Thing(new Vector2f(200, 80));
            List<Collectable> reward = new List<Collectable>();
            reward.Add(new GoldCoin());
            Quest quest = new Quest(new TaskToComplete(typeof(Thing), 3, new List<bool>()), reward, "test", "collect the Thing", 1);

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
                        Console.WriteLine(lol);
                    }

                player.Update(map.GetRectangles());

                if (player.Mask.Intersects(thing1.mask))
                {
                    inventory.Add(thing1.collect(), 1);
                    lol++;
                }
                if (player.Mask.Intersects(gold.mask))
                {
                    inventory.Add(gold.collect(), 1);
                    lol++;
                }
                if (player.Mask.Intersects(thing2.mask))
                {
                    inventory.Add(thing2.collect(), 1);
                    lol++;
                }
                if (player.Mask.Intersects(thing3.mask))
                {
                    inventory.Add(thing3.collect(), 1);
                    lol++;
                }
                if (player.Mask.Intersects(thing4.mask))
                {
                    inventory.Add(thing4.collect(), 1);
                    lol++;
                }

                window.SetView(view);

                window.Clear();

                if (quest.CheckTask(inventory))
                {
                    inventory.Add(quest.getReward()[0], quest.getReward().Count);
                    inventory.Remove(quest.giveCost()[0], quest.giveCost().Count + 1);
                }

                map.Draw(window,view);
                player.Draw(window);
                gold.Draw(window);
                thing1.Draw(window);
                thing2.Draw(window);
                thing3.Draw(window);
                thing4.Draw(window);

                for (int i = 0; i < inventory.Size();i++)
                {
                    inventory.GetCollectable(i).position = new Vector2f(i * 40, 0);
                    inventory.GetCollectable(i).Draw(window);
                }

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

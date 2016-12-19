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

            //Load Spritemaps
            ContentManager.Load();
            QuestManager.currentQuests = new List<Quest>();

            //testing stuff
            //Create Collectables
            List<Collectable> collectables = new List<Collectable>();
            collectables.Add(new Mushroom(new Vector2f(150, 500)));
            collectables.Add(new Mushroom(new Vector2f(700, 500)));
            collectables.Add(new Mushroom(new Vector2f(700, 200)));
            collectables.Add(new Mushroom(new Vector2f(500, 200)));


            //Create NPCS
            List<Collectable> reward = new List<Collectable>();
            reward.Add(new GoldCoin());
            List<Npc> npcs = new List<Npc>();
            npcs.Add(new Npc("lol", 0, new Vector2f(23, 23), new Vector2f(200, 2500), new Quest(new TaskToComplete(typeof(Mushroom), 3), reward, "test", "collect three Mushrooms", 1)));

            //Load Map
            Map map = new Map("fantasieWorld.tmx");
            List<FloatRect> collisionBlocks = new List<FloatRect>();
            for (int i = 0; i < map.GetRectangles().Count; i++)
                collisionBlocks.Add(map.GetRectangles()[i]);
            for (int i = 0; i < npcs.Count; i++)
                collisionBlocks.Add(npcs[i].Mask);


            Player player = new Player("lol", 0, 10, new Vector2f(23, 23), new Vector2f(300, 2500));
            Inventory inventory = new Inventory();

            while (true)
            {
                //Map movement
                updateView(view, player, map);


                // ****** Update *************************************************************************

                player.Update(collisionBlocks, npcs, collectables, inventory);
                npcs[0].Update(map.GetRectangles());


                // ****** Draw ***************************************************************************

                window.SetView(view);

                window.Clear();

                map.Draw(window, view);
                player.Draw(window);

                for (int i = 0; i < collectables.Count; i++)
                    collectables[i].Draw(window);

                npcs[0].Draw(window);
                QuestManager.DrawQuests(window, view, inventory);
                inventory.Draw(window, view);

                window.Display();
            }
        }

        static void updateView(View view, Player player,Map map)
        {
            view.Center = player.Position();
            if (view.Center.X - view.Size.X / 2 < 0)
                view.Center = new Vector2f(view.Size.X / 2, view.Center.Y);
            if (view.Center.X + view.Size.X / 2 > map.width * map.tilewidth)
                view.Center = new Vector2f(map.width * map.tilewidth - view.Size.X / 2, view.Center.Y);
            if (view.Center.Y - view.Size.Y / 2 < 0)
                view.Center = new Vector2f(view.Center.X, view.Size.Y / 2);
            if (view.Center.Y + view.Size.Y / 2 > map.height * map.tileheight)
                view.Center = new Vector2f(view.Center.X, map.height * map.tileheight - view.Size.Y / 2);
        }
    }
}

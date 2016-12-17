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
            collectables.Add(new Thing(new Vector2f(100, 50)));

            //Create NPCS
            List<Collectable> reward = new List<Collectable>();
            reward.Add(new GoldCoin());
            List<Npc> npcs = new List<Npc>();
            npcs.Add(new Npc("lol", 0, new Vector2f(23, 23), new Vector2f(50, 50), new Quest(new TaskToComplete(typeof(Thing), 3), reward, "test", "collect the Thing", 1)));

            //Load Map
            Map map = new Map("fantasieWorld.tmx");
            List<FloatRect> collisionBlocks = new List<FloatRect>();
            for (int i = 0; i < map.GetRectangles().Count; i++)
                collisionBlocks.Add(map.GetRectangles()[i]);
            for (int i = 0; i < npcs.Count; i++)
                collisionBlocks.Add(npcs[i].Mask);


            Player player = new Player("lol", 0, 10, new Vector2f(23, 23), new Vector2f(0, 0));
            Inventory inventory = new Inventory();

            while (true)
            {
                //Map movement
                view.Center = player.Position();

                // ****** Update *************************************************************************

                player.Update(collisionBlocks,npcs, collectables,inventory);
                npcs[0].Update(map.GetRectangles());
                

                // ****** Draw ***************************************************************************

                window.SetView(view);

                window.Clear();

                map.Draw(window,view);
                player.Draw(window);

                for (int i = 0; i < collectables.Count; i++)
                    collectables[i].Draw(window);
                npcs[0].Draw(window);
                QuestManager.DrawQuests(window, view, inventory);
                inventory.Draw(window, view);

                window.Display();
            }
        }
    }
}

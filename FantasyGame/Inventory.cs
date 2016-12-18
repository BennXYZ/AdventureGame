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
    class Inventory
    {
        private List<Collectable> collectables;

        public List<Collectable> Collectables
        {
            get
            {
                return collectables;
            }
        }

        public Inventory()
        {
            collectables = new List<Collectable>();
        }

        public int Size()
        {
            return Collectables.Count;
        }

        public Collectable GetCollectable(int index)
        {
            return Collectables[index];
        }

        public int GetIndexOf(Type type)
        {
            for (int i = 0; i < collectables.Count; i++)
                if (collectables[i].GetType() == type)
                    return i;
            return -1;
        }

        public void Remove(Type item, int amount)
        {
            for (int a = 0; a < amount; a++)
                collectables.RemoveAt(GetIndexOf(item));
            sortInventory();
        }

        public void Draw(RenderWindow window, View view)
        {
            for(int i = 0; i < Collectables.Count; i++)
            {
                Collectables[i].position = new Vector2f(view.Center.X - (view.Size.X / 2) + 10 + (40 * i), view.Center.Y - (view.Size.Y / 2) + 10);
                Collectables[i].Draw(window);
            }
        }

        public void Add(Collectable item, int amount)
        {
            for(int i = 0; i < amount; i++)
            {
                Collectables.Add(item);
            }
            sortInventory();
        }

        public int AmountOf(Type item)
        {
            int c = 0;
            for(int i = 0; i < Collectables.Count; i++)
            {
                if (Collectables[i].GetType() == item)
                    c++;
            }
            return c;
        }

        private void sortInventory()    //TODO: sortiert die Objekte in 'collectables' nach id (ein Collectable hat eine ID)
        {
            List<Collectable> temp = new List<Collectable>();

            for (int a = collectables.Count - 1; a > 0; a--)
                for(int b = 0; b < a; b++)
                    if (collectables[b].id > collectables[b + 1].id)
                    {
                        temp.Add(collectables[b]);
                        collectables[b] = collectables[b + 1];
                        collectables[b + 1] = temp[0];
                    }
        }
    }
}

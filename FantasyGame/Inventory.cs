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

        public void Remove(Type item, int amount)
        {

            for(int i = Collectables.Count - 1; i > -1; i--)
            {
                if (Collectables[i].GetType() == item)
                    Collectables.RemoveAt(i);
            }
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

        }
    }
}

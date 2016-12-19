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
        /// <summary>
        /// List of Collectables. This is the ACTUAL Inventory that contains all the Items
        /// </summary>
        private List<Collectable> collectables;

        /// <summary>
        /// Used to get the Content within the Inventory
        /// </summary>
        public List<Collectable> Collectables
        {
            get
            {
                return collectables;
            }
        }

        /// <summary>
        /// Creates a Inventory... Yes it just creates a new List
        /// </summary>
        public Inventory()
        {
            collectables = new List<Collectable>();
        }

        /// <summary>
        /// How many Items are within the Inventory
        /// </summary>
        /// <returns>returns an int showing the Count of the Collectables-List</returns>
        public int Size()
        {
            return Collectables.Count;
        }

        /// <summary>
        /// Used to get the Collectable at a List-Index. Usefull to look through the Inventory
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Collectable GetCollectable(int index)
        {
            return Collectables[index];
        }

        /// <summary>
        /// Searches for a specific Collectable in the Inventory
        /// </summary>
        /// <param name="type">Type if Collectable that you are looking for. Only use Collectables, since List can contain nothing else</param>
        /// <returns>Returns index of the first found Collectable of the type</returns>
        public int GetIndexOf(Type type)
        {
            for (int i = 0; i < collectables.Count; i++)
                if (collectables[i].GetType() == type)
                    return i;
            return -1;
        }

        /// <summary>
        /// Used to remove Collectables from the Inventory
        /// </summary>
        /// <param name="item">type of Collectable you want to remove</param>
        /// <param name="amount">how many of the wanted types should be removed</param>
        public void Remove(Type item, int amount)
        {
            for (int a = 0; a < amount; a++)
                collectables.RemoveAt(GetIndexOf(item));
            sortInventory();
        }

        /// <summary>
        /// Draws the Items in the Inventory in the upper left Corner
        /// </summary>
        public void Draw(RenderWindow window, View view)
        {
            for(int i = 0; i < Collectables.Count; i++)
            {
                Collectables[i].position = new Vector2f(view.Center.X - (view.Size.X / 2) + 10 + (40 * i), view.Center.Y - (view.Size.Y / 2) + 10);
                Collectables[i].Draw(window);
            }
        }

        /// <summary>
        /// Adds Collectables to the List
        /// </summary>
        /// <param name="item">collectable you want to add</param>
        /// <param name="amount">how many of the collectable you want to add</param>
        public void Add(Collectable item, int amount)
        {
            for(int i = 0; i < amount; i++)
            {
                Collectables.Add(item);
            }
            sortInventory();
        }

        /// <summary>
        /// Used to get the amount of a specific Collectable within the Inventory
        /// </summary>
        /// <param name="item">type of Collectable</param>
        /// <returns>Returns an int of how many collectables have been found</returns>
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

        /// <summary>
        /// Used to sort the Collectables in the inventory by their IDs after adding/removing one. Uses Bubblesort
        /// </summary>
        private void sortInventory()
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

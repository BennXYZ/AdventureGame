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

        public Inventory()
        {
            collectables = new List<Collectable>();
        }

        public void Remove(Type item, int amount)
        {

            for(int i = collectables.Count - 1; i > -1; i--)
            {
                if (collectables[i].GetType() == item)
                    collectables.RemoveAt(i);
            }
        }

        public void Add(Collectable item, int amount)
        {
            for(int i = 0; i < amount; i++)
            {
                collectables.Add(item);
            }
        }

        public int AmountOf(Type item)
        {
            int c = 0;
            for(int i = 0; i < collectables.Count; i++)
            {
                if (collectables[i].GetType() == item)
                    c++;
            }
            return c;
        }
    }
}

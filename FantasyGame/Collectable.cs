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
    abstract class Collectable
    {
        public string name;
        public FloatRect mask;
        public Vector2f position;
        public int id;

        /// <summary>
        /// Used when collecting a Collectable that lies on the map. removes the collectable after usage.
        /// </summary>
        /// <returns>Returns the Collectable. Used to add to the Inventory</returns>
        abstract public Collectable collect();

        /// <summary>
        /// Used to draw the Collectable. Used by Collectable-List and Inventory
        /// </summary>
        /// <param name="window"></param>
        abstract public void Draw(RenderWindow window);
    }

    //ID = 1
    class GoldCoin: Collectable
    {
        int sprite;
        int spriteMapId;

        /// <summary>
        /// Creates a Goldcoin. Mainly used by the Inventory and Quests
        /// </summary>
        public GoldCoin()
        {
            name = "Gold Coin";
            sprite = 1;
            id = 1;

            for (int r = 0; r < ContentManager.spriteMaps.Count; r++)
            {
                if ("collectables" == ContentManager.spriteMaps[r].name)
                {
                    spriteMapId = r;
                    break;
                }
            }
        }

        /// <summary>
        /// Creates Goldcoin with Position. Used to create Goldcoins that lie around on the map
        /// </summary>
        /// <param name="position">position whrere the Gold Coin lies.</param>
        public GoldCoin(Vector2f position)
        {
            name = "Gold Coin";
            sprite = 1;
            this.position = position;

            for (int r = 0; r < ContentManager.spriteMaps.Count; r++)
            {
                if ("collectables" == ContentManager.spriteMaps[r].name)
                {
                    spriteMapId = r;
                    break;
                }
            }
            mask = new FloatRect(new Vector2f(position.X, position.Y),
                    new Vector2f(ContentManager.spriteMaps[spriteMapId].width, ContentManager.spriteMaps[spriteMapId].width));
        }

        /// <summary>
        /// Used when collecting a GoldCoin that lies on the map. removes the GoldCoin after usage.
        /// </summary>
        /// <returns>Returns the GoldCoin. Used to add to the Inventory</returns>
        override public Collectable collect()
        {
            GoldCoin goldcoin = new GoldCoin();
            mask = new FloatRect(new Vector2f(0, 0), new Vector2f(0, 0));
            sprite = 0;
            return goldcoin;
        }

        /// <summary>
        /// Draws the GoldCoin at it's current Position (upper left corner if within Inventory)
        /// </summary>
        override public void Draw(RenderWindow window)
        {
            ContentManager.spriteMaps[spriteMapId].Sprites[sprite].Position = position;
            window.Draw(ContentManager.spriteMaps[spriteMapId].Sprites[sprite]);
        }
    }

    //ID = 2
    class Mushroom : Collectable
    {
        int sprite;
        int spriteMapId;

        /// <summary>
        /// Creates a Mushroom. Mainly used by the Inventory and Quests
        /// </summary>
        public Mushroom()
        {
            name = "Mushroom";
            sprite = 2;
            id = 2;

            for (int r = 0; r < ContentManager.spriteMaps.Count; r++)
            {
                if ("collectables" == ContentManager.spriteMaps[r].name)
                {
                    spriteMapId = r;
                    break;
                }
            }
        }

        /// <summary>
        /// Creates Mushroom with Position. Used to create Mushrooms that lie around on the map
        /// </summary>
        /// <param name="position">position whrere the Mushroom lies.</param>
        public Mushroom(Vector2f position)
        {
            name = "Mushroom";
            sprite = 2;
            id = 2;
            this.position = position;

            for (int r = 0; r < ContentManager.spriteMaps.Count; r++)
            {
                if ("collectables" == ContentManager.spriteMaps[r].name)
                {
                    spriteMapId = r;
                    break;
                }
            }
            mask = new FloatRect(new Vector2f(position.X, position.Y),
                    new Vector2f(ContentManager.spriteMaps[spriteMapId].tilewidth, ContentManager.spriteMaps[spriteMapId].tileheight));
        }

        /// <summary>
        /// Used when collecting a Mushroom that lies on the map. removes the Mushroom after usage.
        /// </summary>
        /// <returns>Returns the Mushroom. Used to add to the Inventory</returns>
        override public Collectable collect()
        {
            Mushroom temp = new Mushroom();
            mask = new FloatRect(new Vector2f(0, 0), new Vector2f(0, 0));
            sprite = 0;
            return temp;
        }

        /// <summary>
        /// Draws the Mushroom at it's current Position (upper left corner if within Inventory)
        /// </summary>
        override public void Draw(RenderWindow window)
        {
            ContentManager.spriteMaps[spriteMapId].Sprites[sprite].Position = position;
            window.Draw(ContentManager.spriteMaps[spriteMapId].Sprites[sprite]);
        }
    }
}
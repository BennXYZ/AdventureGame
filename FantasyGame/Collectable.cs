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

        abstract public Collectable collect();

        abstract public void Draw(RenderWindow window);
    }

    //ID = 1
    class GoldCoin: Collectable
    {
        int sprite;
        int spriteMapId;

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

        override public Collectable collect()
        {
            GoldCoin goldcoin = new GoldCoin();
            mask = new FloatRect(new Vector2f(0, 0), new Vector2f(0, 0));
            sprite = 0;
            return goldcoin;
        }

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

        override public Collectable collect()
        {
            Mushroom temp = new Mushroom();
            mask = new FloatRect(new Vector2f(0, 0), new Vector2f(0, 0));
            sprite = 0;
            return temp;
        }

        override public void Draw(RenderWindow window)
        {
            ContentManager.spriteMaps[spriteMapId].Sprites[sprite].Position = position;
            window.Draw(ContentManager.spriteMaps[spriteMapId].Sprites[sprite]);
        }
    }
}
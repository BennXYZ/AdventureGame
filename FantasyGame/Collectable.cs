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
        int sprite;
        Vector2f position;
        public FloatRect mask;

        public Collectable collect()
        {
            sprite = 0;
            position = new Vector2f(0, 0);
            return this;
        }
    }

    class GoldCoin: Collectable
    {
        public string name;
        int sprite;
        int spriteMapId;
        Vector2f position;
        public FloatRect mask;

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

        public Collectable collect()
        {
            position = new Vector2f(0, 0);
            return this;
        }

        public void Draw(RenderWindow window)
        {
            ContentManager.spriteMaps[spriteMapId].Sprites[sprite].Position = position;
            window.Draw(ContentManager.spriteMaps[spriteMapId].Sprites[sprite]);
        }
    }

    class Thing : Collectable
    {
        public string name;
        int sprite;
        int spriteMapId;
        Vector2f position;
        public FloatRect mask;

        public Thing(Vector2f position)
        {
            name = "Thing";
            sprite = 2;
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

        public Collectable collect()
        {
            Thing thing = this;
            mask = new FloatRect(new Vector2f(0, 0), new Vector2f(0, 0));
            sprite = 0;
            Console.WriteLine(thing.name);
            return thing;
        }

        public void Draw(RenderWindow window)
        {
            ContentManager.spriteMaps[spriteMapId].Sprites[sprite].Position = position;
            window.Draw(ContentManager.spriteMaps[spriteMapId].Sprites[sprite]);
        }
    }
}
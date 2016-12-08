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
        string name;
        int[,] sprite;
    }

    class GoldCoin: Collectable
    {
        string name;
        int sprite;
        int spriteMapId;

        public GoldCoin()
        {
            name = "Gold Coin";
            sprite = 0;

            for (int r = 0; r < ContentManager.spriteMaps.Count; r++)
            {
                if ("collectables" == ContentManager.spriteMaps[r].name)
                    spriteMapId = r;
                break;
            }
        }

        public void Draw(RenderWindow window, Vector2f position)
        {
            ContentManager.spriteMaps[spriteMapId].Sprites[sprite].Position = position;
            window.Draw(ContentManager.spriteMaps[spriteMapId].Sprites[sprite]);
        }
    }
}
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace FantasyGame
{
    static class ContentManager
    {
        public static List<SpriteMap> spriteMaps = new List<SpriteMap>();
        public static Font arial;

        public static void LoadFont()
        {
            arial = new Font("arial.ttf");
        }
    }

        class SpriteMap
        {
            /// <summary>
            /// Creates a Spritemap which contains a Array of Sprites used for Drawing Maps and Animations
            /// </summary>
            /// <param name="id">Id of the SpriteMap</param>
            /// <param name="name">Name for the SpriteMap</param>
            /// <param name="fileName">Name of the Texture</param>
            /// <param name="tilewidth">Width of a Sprite</param>
            /// <param name="tileheight">Height of a Sprite</param>
            /// <param name="width">Amount of Horizontal Sprites</param>
            /// <param name="height">Amount of Vertical Sprites</param>
            public SpriteMap(int id, string name, string fileName, int tilewidth, int tileheight)
            {
                this.id = id;
                this.name = name;
                int height;
                Texture texture = new Texture(fileName);
                Sprites = new List<Sprite>();

                width = (int)(texture.Size.X / tilewidth);
                height = (int)(texture.Size.Y / tileheight);

                for (int i = 0; i < (width * height) + 1; i++)
                {
                    if (i == 0)
                        Sprites.Add(new Sprite((texture), new IntRect(new Vector2i(0, 0), new Vector2i(0, 0)))); //adds an empty tile
                    else
                        Sprites.Add(new Sprite(texture, new IntRect(new Vector2i(((i - 1) % width) * tilewidth, ((i - 1) / width) * tileheight),
                            new Vector2i(tilewidth, tileheight))));          //turns single tiles of a tileset into seperate sprites
                }
            }

            /// <summary>
            /// width describes the amount of Sprites the Spritemap has within one column
            /// </summary>
            public int width { get; }
            public int id { get; }
            public string name { get; }
            public List<Sprite> Sprites { get; }
        }
    
}

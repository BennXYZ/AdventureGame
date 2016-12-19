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
        /// <summary>
        /// List of Spritemaps than have a number of single Sprites. Contains Spritemaps of Environment-Textures, Player-Spritesheets, etc.
        /// </summary>
        public static List<SpriteMap> spriteMaps = new List<SpriteMap>();
        public static Font arial;

        /// <summary>
        /// Loads the Font.
        /// </summary>
        public static void LoadFont()
        {
            arial = new Font("arial.ttf");
        }

        /// <summary>
        /// Loads the Textures and turns them into Spritemaps.
        /// </summary>
        public static void Load()
        {
            spriteMaps.Add(new SpriteMap(0, "base_out_atlas", "base_out_atlas.png", 32, 32));
            spriteMaps.Add(new SpriteMap(0, "terrain_atlas", "terrain_atlas.png", 32, 32));
            spriteMaps.Add(new SpriteMap(0, "houses", "houses.png", 32, 32));
            spriteMaps.Add(new SpriteMap(0, "player", "player.png", 23, 23));
            spriteMaps.Add(new SpriteMap(0, "collectables", "collectables.png", 28, 28));
            spriteMaps.Add(new SpriteMap(0, "questBG", "questBG.png", 399, 202));
            LoadFont();
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
            sprites = new List<Sprite>();
            this.tilewidth = tilewidth;
            this.tileheight = tileheight;

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
        /// <summary>
        /// Id of the Spritemap. Hasn't been necessary yet, since all Spritemaps are being found by their name
        /// </summary>
        public int id { get; }
        public int tilewidth { get; }
        public int tileheight { get; }
        public string name { get; }

        /// <summary>
        /// Used to get the Sprites of the Spritemap
        /// </summary>
        public List<Sprite> Sprites
        {
            get
            {
                return sprites;
            }
        }

        /// <summary>
        /// The actuall List of Sprites. All Sprites that are being drawn are taken out of this List
        /// </summary>
        private List<Sprite> sprites;
    }
    
}

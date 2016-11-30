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

namespace LoadXML
{
    [XmlRoot("map")]
    public class Map
    {
        #region Methods

        public Map()
        {}

        /// <summary>
        /// Initializes the Map
        /// </summary>
        /// <param name="xmlFile">.XML / .TMX File or Path</param>
        public Map(string xmlFile)
        {
            StreamReader reader = new StreamReader(xmlFile);
            XmlSerializer serializer = new XmlSerializer(typeof(Map));

            Map map = (Map)serializer.Deserialize(reader);
            width = map.width;
            height = map.height;
            tilewidth = map.tilewidth;
            tileheight = map.tileheight;
            layers = map.layers;
            objectgroups = map.objectgroups;
            tilesets = map.tilesets;

            AddTiles();
        }

        /// <summary>
        /// Initializing-Logic
        /// </summary>
        private void AddTiles()
        {
            Tiles = new List<int[,]>();
            spriteMap = new List<int>();

            for (int s = 0; s < ContentManager.spriteMaps.Count; s++)
            {
                for (int t = 0; t < tilesets.Length; t++)
                {
                    if (ContentManager.spriteMaps[s].name == tilesets[t].name)
                        spriteMap.Add(s);
                }
            }

            for (int i = 0; i < layers.Length; i++)
            {
                Tiles.Add(CreateTiles(i));
            }
        }

        /// <summary>
        /// Returns a 2D-Array with the int ID's of the Tiles
        /// </summary>
        /// <param name="layer">The Layer of the wanted Tiles</param>
        private int[,] CreateTiles(int layer)
        {
            int[,] tiles = new int[width, height]; ;
            for (int y = 0; y < height+1; y++)
            {
                string line = layers[layer].tileString.Split('\n')[y];
                for (int x = 0; x < width; x++)
                {
                    if (y > 0)
                    {
                        int id = Convert.ToInt32(line.Split(',')[x]);
                        tiles[x, y - 1] = id;
                    }
                }
            }
            return tiles;
        }

        /// <summary>
        /// Returns the Tiles within the wanted Layer of the Map
        /// </summary>
        public int[,] GetTiles(int layer)
        {
            return Tiles[layer];
        }

        /// <summary>
        /// Updates the Map
        /// </summary>
        public void Update()
        {

        }

        /// <summary>
        /// Draws the Map
        /// </summary>
        public void Draw(RenderWindow window, View view)
        {
            for (int i = 0; i < Tiles.Count ; i++)
            {
                for (int y = ((int)(view.Center.Y - (view.Size.Y / 2)) / tileheight); y < ((int)(view.Center.Y + (view.Size.Y / 2)) / tileheight + 1); y++)
                {
                    for (int x = ((int)(view.Center.X- (view.Size.X / 2)) / tilewidth); x < ((int)(view.Center.X + (view.Size.X / 2)) / tilewidth + 1); x++)
                    {
                        if (-1 < x && x < width && -1 < y && y < height)
                        {
                            int currentSpriteMap = 0;
                            for (int r = 0; r < tilesets.Length; r++)
                                if (Tiles[i][x, y] >= tilesets[r].firstgid)
                                    currentSpriteMap = r;

                            ContentManager.spriteMaps[spriteMap[currentSpriteMap]].Sprites[Tiles[i][x, y] - (tilesets[currentSpriteMap].firstgid - 1)].Position = new Vector2f(x * tilewidth, y * tileheight);
                            window.Draw(ContentManager.spriteMaps[spriteMap[currentSpriteMap]].Sprites[Tiles[i][x,y] - (tilesets[currentSpriteMap].firstgid - 1)]);
                        }

                    }
                }
            }
        }

        private int GetSpritemap(int tile)
        {
            string lol;

            return 0;
        }

        #endregion

        #region Variables

        List<int> spriteMap;

        List<int[,]> Tiles;

        [XmlAttribute("width")]
        public int width
        {
            get;
            set;
        }

        [XmlAttribute("height")]
        public int height
        {
            get;
            set;
        }

        [XmlAttribute("tilewidth")]
        public int tilewidth
        {
            get;
            set;
        }

        [XmlAttribute("tileheight")]
        public int tileheight
        {
            get;
            set;
        }

        [XmlElement("layer")]
        public Layer[] layers
        {
            get;
            set;
        }

        [XmlElement("objectgroup")]
        public ObjectGroup[] objectgroups
        {
            get;
            set;
        }

        [XmlElement("tileset")]
        public Tileset[] tilesets
        {
            get;
            set;
        }

        #endregion
    }

    public class Layer
    {
        [XmlAttribute("name")]
        public string name
        {
            get;
            set;
        }

        [XmlElement("data")]
        public string tileString
        {
            get;
            set;
        }
    }

    public class ObjectGroup
    {
        [XmlAttribute("name")]
        public string name
        {
            get;
            set;
        }

        [XmlElement("object")]
        public Rectangle[] rectangles
        {
            get;
            set;
        }
    }

    public class Tileset
    {
        [XmlAttribute("name")]
        public string name
        {
            get;
            set;
        }

        [XmlAttribute("firstgid")]
        public int firstgid
        {
            get;
            set;
        }
    }

    public class Rectangle
    {
        [XmlAttribute("id")]
        public int id
        {
            get;
            set;
        }

        [XmlAttribute("x")]
        public int x
        {
            get;
            set;
        }

        [XmlAttribute("y")]
        public int y
        {
            get;
            set;
        }

        [XmlAttribute("width")]
        public int width
        {
            get;
            set;
        }

        [XmlAttribute("height")]
        public int height
        {
            get;
            set;
        }
    }
}

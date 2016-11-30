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
    [XmlRoot("map")]
    public class Map
    {
        #region PublicMethods

        /// <summary>
        /// Only for XML. Use the other initializing-Method.
        /// </summary>
        private Map()
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
            CreateCollisions();
        }

        /// <summary>
        /// Returns the Tiles within the wanted Layer of the Map
        /// </summary>
        public int[,] GetTiles(int layer)
        {
            return Tiles[layer];
        }

        public List<FloatRect> GetRectangles()
        {
            return collisions;
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
                            for (int r = 0; r < tilesets.Length; r++)

                            ContentManager.spriteMaps[spriteMap[GetSpritemap(Tiles[i][x, y])]].Sprites[Tiles[i][x, y] - (tilesets[GetTexture(Tiles[i][x, y])].firstgid - 1)].Position = new Vector2f(x * tilewidth, y * tileheight);
                            window.Draw(ContentManager.spriteMaps[spriteMap[GetSpritemap(Tiles[i][x, y])]].Sprites[Tiles[i][x,y] - (tilesets[GetTexture(Tiles[i][x, y])].firstgid - 1)]);
                        }
                    }
                }
            }
        }

        #endregion

        #region PrivateMethods

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
            for (int y = 0; y < height + 1; y++)
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

        private int GetTexture(int tile)
        {
            for (int r = 0; r < tilesets.Length - 1; r++) // geht alle Tilesets durch
            {
                if (tile < tilesets[r + 1].firstgid)    //bricht ab wenn das passende Tileset gefunden wurde
                    return r;
            }
            return (tilesets.Length - 1);
        }

        /// <summary>
        /// Gets the wanted Spritemap. Necesary so that you don't have to put the spritemaps in order
        /// </summary>
        private int GetSpritemap(int tile)
        {
            for (int r = 0; r < ContentManager.spriteMaps.Count; r++)
            {
                if (tilesets[GetTexture(tile)].name == ContentManager.spriteMaps[r].name)
                    return r;
            }
            throw new FileNotFoundException("There is no Spritemap with the same name as the tilesets");
        }

        /// <summary>
        /// Turns collidable Tiles and manually creates Objects into Rectangles
        /// </summary>
        private void CreateCollisions()
        {
            collisions = new List<FloatRect>();

            for (int i = 0; i < objectgroups.Length; i++)
            {
                for (int r = 0; r < objectgroups[i].rectangles.Length; r++)
                {
                    collisions.Add(new FloatRect(objectgroups[i].rectangles[r].x, objectgroups[i].rectangles[r].y, objectgroups[i].rectangles[r].width, objectgroups[i].rectangles[r].height));
                }
            }

            for(int l = 0; l < Tiles.Count; l++)
                for (int x = 0; x < Tiles[l].GetLength(0); x++)
                    for (int y = 0; y < Tiles[l].GetLength(1); y++)
                        for (int t = 0; t < tilesets.Length; t++)
                            for (int r = 0; r < tilesets[t].collTiles.Length; r++)
                            {
                            if (Tiles[l][x, y] == (Convert.ToInt32(tilesets[t].collTiles[r].Id) + 1))
                                collisions.Add(new FloatRect(x * tilewidth, y * tileheight, tilewidth, tileheight));
                        }
        }

        #endregion

        #region Variables

        List<FloatRect> collisions;

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

    public class Tile
    {
        [XmlAttribute("id")]
        public string Id
        {
            get;
            set;
        }
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

        [XmlElement("tile")]
        public Tile[] collTiles
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

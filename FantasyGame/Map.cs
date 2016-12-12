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
        /// Initializes a drawable Map with rectangles for collisions
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
        /// Gets a Layer of Tiles
        /// </summary>
        /// <param name="layer">the layer you want to get</param>
        /// <returns>Returns a 2D-array of int</returns>
        public int[,] GetTiles(int layer)
        {
            return tileLayers[layer];
        }

        /// <summary>
        /// gets the rectangles
        /// </summary>
        /// <returns>Returns a List of Rectangles that can be used for collisions with characters</returns>
        public List<FloatRect> GetRectangles()
        {
            return collisions;
        }

        /// <summary>
        /// Updates the Map. Doesn't really do anything...
        /// </summary>
        public void Update()
        {

        }

        /// <summary>
        /// Draws the map. use between window.Clear and window.Display
        /// </summary>
        /// <param name="window">Takes the window of the main-program</param>
        /// <param name="view">Takes the Viewport of the main-program</param>
        public void Draw(RenderWindow window, View view)
        {
            for (int i = 0; i < tileLayers.Count ; i++)
            {
                for (int y = ((int)(view.Center.Y - (view.Size.Y / 2)) / tileheight); y < ((int)(view.Center.Y + (view.Size.Y / 2)) / 
                    tileheight + 1); y++)
                {
                    for (int x = ((int)(view.Center.X- (view.Size.X / 2)) / tilewidth); x < ((int)(view.Center.X + (view.Size.X / 2)) / 
                        tilewidth + 1); x++)
                    {
                        if (-1 < x && x < width && -1 < y && y < height)
                        {
                            ContentManager.spriteMaps[spriteMap[GetSpritemap(tileLayers[i][x, y])]].Sprites[tileLayers[i][x, y] - 
                                (tilesets[GetTexture(tileLayers[i][x, y])].firstgid - 1)].Position = new Vector2f(x * tilewidth, y * tileheight);
                            window.Draw(ContentManager.spriteMaps[spriteMap[GetSpritemap(tileLayers[i][x, y])]].Sprites[tileLayers[i][x,y] - 
                                (tilesets[GetTexture(tileLayers[i][x, y])].firstgid - 1)]);
                        }
                    }
                }
            }
        }

        #endregion

        #region PrivateMethods

        /// <summary>
        /// Only for XML. Use the other initializing-Method.
        /// </summary>
        private Map()
        { }

        /// <summary>
        /// memorizes which spritemaps are needed and creates the tilelayers
        /// </summary>
        private void AddTiles()
        {
            tileLayers = new List<int[,]>();
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
                tileLayers.Add(CreateTiles(i));
            }
        }

        /// <summary>
        /// turns the string of the XML-File into a 2D-Array of int
        /// </summary>
        /// <param name="layer">the layer of int[,] you want to create</param>
        /// <returns>returns a 2D-Array of int used to memorize which Tile belongs to which position</returns>
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

        /// <summary>
        /// searches of the tileset that contains a specific Tile. Necessary since not all Tilesets start at 0 (firstgid)
        /// </summary>
        /// <param name="tile">int of the Tile you want the texture of</param>
        /// <returns>returns the id of the texture that is needed</returns>
        private int GetTexture(int tile)
        {
            for (int r = 0; r < tilesets.Length - 1; r++)
            {
                if (tile < tilesets[r + 1].firstgid)    //bricht ab wenn das passende Tileset gefunden wurde
                    return r;
            }
            return (tilesets.Length - 1);
        }

        /// <summary>
        /// searches for the needed Spritemap for the current Tile. Makes it possible to load spriteMaps in any order.
        /// </summary>
        /// <param name="tile">id of the current tile</param>
        /// <returns>returns a int that is used to load a specific Spritemap</returns>
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

            if (objectgroups != null)
            for (int i = 0; i < objectgroups.Length; i++)
            {
                for (int r = 0; r < objectgroups[i].rectangles.Length; r++)
                {
                    collisions.Add(new FloatRect(objectgroups[i].rectangles[r].x, objectgroups[i].rectangles[r].y, objectgroups[i].rectangles[r].width, objectgroups[i].rectangles[r].height));
                }
            }


            for (int l = 0; l < tileLayers.Count; l++)              //TODO: Entfernen und einfügen, dass der Spieler nach Tile abfragt
                for (int x = 0; x < tileLayers[l].GetLength(0); x++)
                    for (int y = 0; y < tileLayers[l].GetLength(1); y++)
                        for (int t = 0; t < tilesets.Length; t++)
                            if (tilesets[t].collTiles != null)
                                for (int r = 0; r < tilesets[t].collTiles.Length; r++)
                                {
                                    if (tileLayers[l][x, y] == (Convert.ToInt32(tilesets[t].collTiles[r].Id) + 1))
                                        collisions.Add(new FloatRect(x * tilewidth, y * tileheight, tilewidth, tileheight));
                                }
        }

        #endregion

        #region Variables

        /// <summary>
        /// List of Rectangles that are used for collisions
        /// </summary>
        private List<FloatRect> collisions;

        /// <summary>
        /// List of int that are used the load the correct spriteMap
        /// </summary>
        private List<int> spriteMap;

        /// <summary>
        /// List of layers that contain a Tile with X and Y position
        /// </summary>
        private List<int[,]> tileLayers;

        /// <summary>
        /// Total number of Tiles in X-Direction
        /// </summary>
        [XmlAttribute("width")]
        public int width
        {
            get;
            set;
        }

        /// <summary>
        /// Total number of Tiles in Y-Direction
        /// </summary>
        [XmlAttribute("height")]
        public int height
        {
            get;
            set;
        }

        /// <summary>
        /// width of a single tile in Pixel
        /// </summary>
        [XmlAttribute("tilewidth")]
        public int tilewidth
        {
            get;
            set;
        }

        /// <summary>
        /// height of a single tile in Pixel
        /// </summary>
        [XmlAttribute("tileheight")]
        public int tileheight
        {
            get;
            set;
        }

        /// <summary>
        /// layers of Tiles. Mainly used for Initializing. Use "tileLayers" for integers
        /// </summary>
        [XmlElement("layer")]
        public Layer[] layers
        {
            get;
            set;
        }

        /// <summary>
        /// layers of Objects. Mainly used for Initalizing. Use "collisions" for Rectangles
        /// </summary>
        [XmlElement("objectgroup")]
        public ObjectGroup[] objectgroups
        {
            get;
            set;
        }

        /// <summary>
        /// list of used Tilesets. Has the ID of the first tile within the tileset and a list of IDs, which tiles have collisions
        /// </summary>
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
        /// <summary>
        /// id of the Tile that gets a Collision
        /// </summary>
        [XmlAttribute("id")]
        public string Id
        {
            get;
            set;
        }
    }

    public class Layer
    {
        /// <summary>
        /// name of the Layer (derp)
        /// </summary>
        [XmlAttribute("name")]
        public string name
        {
            get;
            set;
        }

        /// <summary>
        /// string of the tiles on the map. Needs to be converted into an int-array
        /// </summary>
        [XmlElement("data")]
        public string tileString
        {
            get;
            set;
        }
    }

    public class ObjectGroup
    {
        /// <summary>
        /// name of the objectgroup(derp)
        /// </summary>
        [XmlAttribute("name")]
        public string name
        {
            get;
            set;
        }

        /// <summary>
        /// list of objects within the objectgroup. This is a new class, not an actual Rectangle. Needs to be converted first.
        /// </summary>
        [XmlElement("object")]
        public Rectangle[] rectangles
        {
            get;
            set;
        }
    }

    public class Tileset
    {
        /// <summary>
        /// name of the Tileset. Needed so that the map knows which spritemap it has to load
        /// </summary>
        [XmlAttribute("name")]
        public string name
        {
            get;
            set;
        }

        /// <summary>
        /// describes which ID a Tile must have to load this Tileset
        /// </summary>
        [XmlAttribute("firstgid")]
        public int firstgid
        {
            get;
            set;
        }

        /// <summary>
        /// Array of tiles which get collisions. need to be transformed into rectangles
        /// </summary>
        [XmlElement("tile")]
        public Tile[] collTiles
        {
            get;
            set;
        }
    }

    public class Rectangle
    {
        /// <summary>
        /// Id of the Rectangle because why nawt?
        /// </summary>
        [XmlAttribute("id")]
        public int id
        {
            get;
            set;
        }

        /// <summary>
        /// X-Position of the Rectangle
        /// </summary>
        [XmlAttribute("x")]
        public int x
        {
            get;
            set;
        }

        /// <summary>
        /// Y-Position of the Rectangle
        /// </summary>
        [XmlAttribute("y")]
        public int y
        {
            get;
            set;
        }

        /// <summary>
        /// width of the Rectangle
        /// </summary>
        [XmlAttribute("width")]
        public int width
        {
            get;
            set;
        }

        /// <summary>
        /// Height of the Rectangle
        /// </summary>
        [XmlAttribute("height")]
        public int height
        {
            get;
            set;
        }
    }
}

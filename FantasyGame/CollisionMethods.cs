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
    class CollisionMethods
    {
        /// <summary>
        /// Turns a Rectangle into a 1 pixel thin Rectangle that shows the upper border of the given Rectangle
        /// </summary>
        /// <param name="rectangle">Rectangle you want to get the new Rectangle from</param>
        /// <returns>Returns a 1 pixel thin Rectangle</returns>
        public static FloatRect TopRectangle(FloatRect rectangle)
        {
            rectangle = new FloatRect(new Vector2f(rectangle.Left,rectangle.Top), new Vector2f(rectangle.Width,1));
            return rectangle;
        }

        /// <summary>
        /// Turns a Rectangle into a 1 pixel thin Rectangle that shows the left border of the given Rectangle
        /// </summary>
        /// <param name="rectangle">Rectangle you want to get the new Rectangle from</param>
        /// <returns>Returns a 1 pixel thin Rectangle</returns>
        public static FloatRect LeftRectangle(FloatRect rectangle)
        {
            rectangle = new FloatRect(new Vector2f(rectangle.Left, rectangle.Top), new Vector2f(1, rectangle.Height));
            return rectangle;
        }

        /// <summary>
        /// Turns a Rectangle into a 1 pixel thin Rectangle that shows the bottom border of the given Rectangle
        /// </summary>
        /// <param name="rectangle">Rectangle you want to get the new Rectangle from</param>
        /// <returns>Returns a 1 pixel thin Rectangle</returns>
        public static FloatRect BottomRectangle(FloatRect rectangle)
        {
            rectangle = new FloatRect(new Vector2f(rectangle.Left, rectangle.Top + rectangle.Height - 1), new Vector2f(rectangle.Width, 1));
            return rectangle;
        }

        /// <summary>
        /// Turns a Rectangle into a 1 pixel thin Rectangle that shows the right border of the given Rectangle
        /// </summary>
        /// <param name="rectangle">Rectangle you want to get the new Rectangle from</param>
        /// <returns>Returns a 1 pixel thin Rectangle</returns>
        public static FloatRect RightRectangle(FloatRect rectangle)
        {
            rectangle = new FloatRect(new Vector2f(rectangle.Left + rectangle.Width - 1, rectangle.Top), new Vector2f(1, rectangle.Height));
            return rectangle;
        }

        /// <summary>
        /// Checks if Two Lists of Rectangles Intersect at some point.
        /// </summary>
        /// <param name="listA">first List of Rectangle</param>
        /// <param name="listB">second List of Rectangle</param>
        /// <returns>if one of the Rectangles of both Lists Interset, this returns true</returns>
        public static bool CheckCollision(List<FloatRect> listA, List<FloatRect> listB)
        {
            for (int a = 0; a < listA.Count; a++)
            {
                for (int b = 0; b < listB.Count; b++)
                {
                    if (listA[a].Intersects(listB[b]))
                        return true; //Breaks if one of the Rectangles intersect
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if A Lists of Rectangles Intersect with a single Rectangle.
        /// </summary>
        /// <param name="list">List of Rectangle</param>
        /// <param name="rectangle">single Rectangle</param>
        /// <returns>if one of the Rectangles of the Lists Intersects with the single Rectangle, this returns true</returns>
        public static bool CheckCollision(List<FloatRect> list, FloatRect rectangle)
        {
            for (int a = 0; a < list.Count; a++)
            {

                if (list[a].Intersects(rectangle))
                    return true;//Breaks if one of the Rectangles intersect

            }
            return false;
        }

        /// <summary>
        /// Rectangles already have this Method but I added it anyways. For completion :3
        /// </summary>
        /// <param name="rectangleA">a single Rectangle</param>
        /// <param name="rectangleB">a single Rectangle</param>
        /// <returns>Returns true if both rectangles intersect with one another</returns>
        public static bool CheckCollision(FloatRect rectangleA, FloatRect rectangleB)
        {
            return rectangleA.Intersects(rectangleB);
        }
    }
}

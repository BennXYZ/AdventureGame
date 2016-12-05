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
        public static FloatRect TopRectangle(FloatRect rectangle)
        {
            rectangle = new FloatRect(new Vector2f(rectangle.Left,rectangle.Top), new Vector2f(rectangle.Width,1));
            return rectangle;
        }

        public static FloatRect LeftRectangle(FloatRect rectangle)
        {
            rectangle = new FloatRect(new Vector2f(rectangle.Left, rectangle.Top), new Vector2f(1, rectangle.Height));
            return rectangle;
        }

        public static FloatRect BottomRectangle(FloatRect rectangle)
        {
            rectangle = new FloatRect(new Vector2f(rectangle.Left, rectangle.Top + rectangle.Height - 1), new Vector2f(rectangle.Width, 1));
            return rectangle;
        }

        public static FloatRect RightRectangle(FloatRect rectangle)
        {
            rectangle = new FloatRect(new Vector2f(rectangle.Left + rectangle.Width - 1, rectangle.Top), new Vector2f(1, rectangle.Height));
            return rectangle;
        }

        public static bool CheckCollision(List<FloatRect> listA, List<FloatRect> listB)
        {
            for (int a = 0; a < listA.Count; a++)
            {
                for (int b = 0; b < listB.Count; b++)
                {
                    if (listA[a].Intersects(listB[b]))
                        return true;
                }
            }
            return false;
        }

        public static bool CheckCollision(List<FloatRect> list, FloatRect rectangle)
        {
            for (int a = 0; a < list.Count; a++)
            {

                if (list[a].Intersects(rectangle))
                    return true;

            }
            return false;
        }

        public static bool CheckCollision(FloatRect rectangleA, FloatRect rectangleB)
        {
            return rectangleA.Intersects(rectangleB);
        }
    }
}

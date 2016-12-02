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
    abstract class Character
    {
        private string name;
        private int id;
        private List<Animation> animations;
        private enum states { };
        private int health;
        private FloatRect mask;
        private Vector2f position, velocity;


        public Character() { }

        public void Update() { }

        public void Draw() { }
    }
}

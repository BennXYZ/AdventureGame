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
    class Player : Character
    {
        private string name;
        private int id;
        private List<Anim> animations;
        private int currentAnimation;
        private enum Estates                                                        //
        { moving = 11, attacking = 21, getHit = 31, NoInput = 91};  //NoInput for during cutscenes or while talking
        private Estates currentState, nextState;
        private int health;
        private FloatRect mask;
        private Vector2f position, velocity;


        public Player(string name, int id, int health, Vector2f size, Vector2f startingPosition)
        {
            this.name = name;
            this.id = id;
            this.health = health;
            position = startingPosition;
            mask = new FloatRect(new Vector2f(position.X - size.X / 2,position.Y - size.Y / 2), new Vector2f(size.X,size.Y));
            currentAnimation = 0;
            velocity = new Vector2f(0, 0);

            animations = new List<Anim>();
        }

        public void addAnimation(int animationID, string spriteMapName, int spriteAmount, int frameAmount, int column)
        {
            animations.Add(new Anim(new Animation(spriteMapName, spriteAmount, frameAmount, column),animationID));
        }

        public void Update()
        {
            UpdateInput();
            Movement();
            CheckCollision();
        }

        private void CheckCollision()
        {
            throw new NotImplementedException();
        }

        private void Movement()
        {
            throw new NotImplementedException();
        }

        private void UpdateInput()
        {
            throw new NotImplementedException();
        }

        public void Draw(RenderWindow window)
        {
            animations[currentAnimation].Animation.Draw(window);
        }


    }

    class Anim
    {
        public Anim(Animation animation, int id)
        {
            this.animation = animation;
            this.id = id;
        }

        private Animation animation;
        private int id;

        public Animation Animation
        {
            get
            {
                return animation;
            }

            set
            {
                animation = value;
            }
        }

        public int ID
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }
    }
}

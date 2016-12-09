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
        { moving = 11, attacking = 21, getHit = 31, NoInput = 91 };  //NoInput for during cutscenes or while talking
        private Estates currentState, nextState;
        private bool iFrames, standing;
        private int health;
        private FloatRect mask, targetMask, examinationMask;
        private Vector2f position, velocity, direction;

        public FloatRect Mask
        {
            get
            {
                return mask;
            }
        }

        public Player(string name, int id, int health, Vector2f size, Vector2f startingPosition)
        {
            this.name = name;
            this.id = id;
            this.health = health;
            position = startingPosition;
            mask = new FloatRect(new Vector2f(position.X, position.Y), new Vector2f(size.X, size.Y));
            targetMask = Mask;
            examinationMask = Mask;
            currentAnimation = 11;
            velocity = new Vector2f(0, 0);
            direction = new Vector2f(0, 0);
            standing = true;

            animations = new List<Anim>();
            animations.Add(new Anim(new Animation("player", 4, 30, 1), 11));
            animations.Add(new Anim(new Animation("player", 4, 30, 2), 12));
            animations.Add(new Anim(new Animation("player", 4, 30, 3), 13));
            animations.Add(new Anim(new Animation("player", 4, 30, 4), 14));

            currentState = Estates.moving;
            nextState = Estates.moving;
        }

        public void addAnimation(int animationID, string spriteMapName, int spriteAmount, int frameAmount, int column)
        {
            animations.Add(new Anim(new Animation(spriteMapName, spriteAmount, frameAmount, column), animationID));
        }

        public void Update(List<FloatRect> collisions)
        {
            UpdateInput();
            Movement();
            CheckCollision(collisions);
            UpdateAnimation();
        }

        private void UpdateAnimation()
        {
            switch (currentState)
            {
                case Estates.moving:
                    if (velocity == new Vector2f(0, 0))
                    {
                        standing = true;
                        Anim.GetAnimID(animations, currentAnimation).Animation.ResetAnimation();

                    }
                    else
                        standing = false;
                    Anim.GetAnimID(animations, currentAnimation).Animation.PauseAnimation(standing);
                    break;
                case Estates.attacking:
                    break;
                case Estates.getHit:
                    break;
                case Estates.NoInput:
                    break;
            }
            Anim.GetAnimID(animations, currentAnimation).Animation.Position = position;
            Anim.GetAnimID(animations, currentAnimation).Animation.Update();
        }

        private void CheckCollision(List<FloatRect> collisions)
        {
            if (CollisionMethods.CheckCollision(collisions, targetMask))
                velocity = new Vector2f(0, 0);

            position = new Vector2f(position.X + velocity.X, position.Y + velocity.Y);

            mask = new FloatRect(new Vector2f(position.X, position.Y), new Vector2f(Mask.Width, Mask.Height));
        }

        private void Movement()
        {
            targetMask = new FloatRect(new Vector2f(position.X, position.Y), new Vector2f(Mask.Width, Mask.Height));

            if (currentState == Estates.moving)
            {
                if (velocity.X > 5)
                {
                    velocity.X = 5;
                }
                if (velocity.Y > 5)
                {
                    velocity.Y = 5;
                }
                if (velocity.X < -5)
                {
                    velocity.X = -5;
                }
                if (velocity.Y < -5)
                {
                    velocity.Y = -5;
                }

                examinationMask.Left = position.X + direction.X * examinationMask.Width;
                examinationMask.Top = position.Y + direction.Y * examinationMask.Height;

                targetMask.Left += velocity.X;
                targetMask.Top += velocity.Y;
            }

            if (currentState == Estates.getHit)
            {
                targetMask.Left -= direction.X;
                targetMask.Top -= direction.Y;
            }
        }

        private void UpdateInput()
        {
            if (currentState == Estates.moving)
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.S))
                {
                    velocity.Y += 0.3f;
                    direction = new Vector2f(0, 1);
                    currentAnimation = 11;
                }
                if (Keyboard.IsKeyPressed(Keyboard.Key.W))
                {
                    velocity.Y -= 0.3f;
                    direction = new Vector2f(0, -1);
                    currentAnimation = 14;
                }
                if (!Keyboard.IsKeyPressed(Keyboard.Key.W) && !Keyboard.IsKeyPressed(Keyboard.Key.S))
                {
                    velocity.Y = 0;
                }
                if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                {
                    velocity.X += 0.3f;
                    direction = new Vector2f(1, 0);
                    currentAnimation = 12;
                }
                if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                {
                    velocity.X -= 0.3f;
                    direction = new Vector2f(- 1, 0);
                    currentAnimation = 13;
                }
                if (!Keyboard.IsKeyPressed(Keyboard.Key.A) && !Keyboard.IsKeyPressed(Keyboard.Key.D))
                    velocity.X = 0;
            }

        }



        public void Draw(RenderWindow window)
        {
            Anim.GetAnimID(animations, currentAnimation).Animation.Draw(window);   //TODO: glitch beim collidieren und Richtungswechselung

            RectangleShape test = new RectangleShape(new Vector2f(examinationMask.Width, examinationMask.Height));
            test.Position = new Vector2f(examinationMask.Left, examinationMask.Top);

            window.Draw(test);
        }


    }


}

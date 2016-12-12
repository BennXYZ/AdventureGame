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
        private enum Estates                                         //NoInput for during cutscenes or while talking   
        { moving = 11, attacking = 21, getHit = 31, NoInput = 91 };  //only needed if combat and cutscenes included
        private Estates currentState;
        private bool standing;
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

        /// <summary>
        /// creates a bastard to play with
        /// </summary>
        /// <param name="name">name of the dude (derp)</param>
        /// <param name="id">id (not sure what for)</param>
        /// <param name="health">health (not needed yet since no combat)</param>
        /// <param name="size">sets the size of the player-Rectangle</param>
        /// <param name="startingPosition">where the player is being creates</param>
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
        }

        /// <summary>
        /// Creates an animation to Draw
        /// </summary>
        /// <param name="animationID">id that is used to load the correct animation</param>
        /// <param name="spriteMapName">name of the player-spritemap</param>
        /// <param name="spriteAmount">how many sprites in Y-Direction the animation has on the tileset</param>
        /// <param name="frameAmount">how many frames it takes for the animation to complete</param>
        /// <param name="column">column of the wanted animation on the tileset, beginning at 1</param>
        public void addAnimation(int animationID, string spriteMapName, int spriteAmount, int frameAmount, int column)
        {
            animations.Add(new Anim(new Animation(spriteMapName, spriteAmount, frameAmount, column), animationID));
        }

        /// <summary>
        /// Updates the Input, Movement, Collisions and Animations
        /// </summary>
        /// <param name="collisions">takes the rectangles the player can collide with</param>
        public void Update(List<FloatRect> collisions)
        {
            UpdateInput();
            Movement();
            CheckCollision(collisions);
            UpdateAnimation();
        }

        /// <summary>
        /// stops animation if player doesn't move and updates the position of the current animation
        /// </summary>
        private void UpdateAnimation()
        {
            if (velocity.Y > velocity.X && velocity.Y > 0)
                currentAnimation = 11;
            if (velocity.Y < velocity.X && velocity.Y < 0)
                currentAnimation = 14;
            if (velocity.Y > velocity.X && velocity.Y == 0)
                currentAnimation = 13;
            if (velocity.Y < velocity.X && velocity.Y == 0)
                currentAnimation = 12;

            if (velocity == new Vector2f(0, 0))
            {
                standing = true;
                Anim.GetAnimID(animations, currentAnimation).Animation.ResetAnimation();
            }
            else
                standing = false;
            Anim.GetAnimID(animations, currentAnimation).Animation.PauseAnimation(standing);

            Anim.GetAnimID(animations, currentAnimation).Animation.Position = position;
            Anim.GetAnimID(animations, currentAnimation).Animation.Update();
        }

        /// <summary>
        /// stops the player if he was about to collide with a collidable tile.
        /// </summary>
        /// <param name="collisions">lust of rectangles the player can collide with</param>
        private void CheckCollision(List<FloatRect> collisions)
        {
            if (CollisionMethods.CheckCollision(collisions, new FloatRect(new Vector2f(mask.Left, targetMask.Top), new Vector2f(targetMask.Width, targetMask.Height))))
                velocity.Y = 0;
            if (CollisionMethods.CheckCollision(collisions, new FloatRect(new Vector2f(targetMask.Left, mask.Top), new Vector2f(targetMask.Width, targetMask.Height))))
                velocity.X = 0;

            position = new Vector2f(position.X + velocity.X, position.Y + velocity.Y);

            mask = new FloatRect(new Vector2f(position.X, position.Y), new Vector2f(Mask.Width, Mask.Height));
        }

        /// <summary>
        /// Slows player down if he goes to fast and creates a targetmask out of position and velocity
        /// </summary>
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
        }

        /// <summary>
        /// updates velocity depending on pressed keys
        /// </summary>
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

        /// <summary>
        /// draws the current animation of the player
        /// </summary>
        /// <param name="window">window of the main-program</param>
        public void Draw(RenderWindow window)
        {
            Anim.GetAnimID(animations, currentAnimation).Animation.Draw(window);

            RectangleShape test = new RectangleShape(new Vector2f(examinationMask.Width, examinationMask.Height)); //TODO: Debug. remove once working
            test.Position = new Vector2f(examinationMask.Left, examinationMask.Top);

            window.Draw(test);
        }


    }


}

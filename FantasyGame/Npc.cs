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
    class Npc : Character
    {
        private string name;
        private int id;
        private List<Anim> animations;
        private int currentAnimation;
        private bool standing;
        private FloatRect mask, targetMask;
        private Vector2f position, velocity;

        public FloatRect Mask
        {
            get
            {
                return mask;
            }
        }

        /// <summary>
        /// Updates the Input, Movement, Collisions and Animations
        /// </summary>
        /// <param name="collisions">takes the rectangles the player can collide with</param>
        public void Update(List<FloatRect> collisions)
        {
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

                targetMask.Left += velocity.X;
                targetMask.Top += velocity.Y;
        }


    }
}

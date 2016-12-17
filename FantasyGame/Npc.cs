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
        private bool standing, questAccepted;
        private FloatRect mask, targetMask;
        private Vector2f position, velocity;
        Quest quest;

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
        /// <param name="size">sets the size of the player-Rectangle</param>
        /// <param name="startingPosition">where the player is being creates</param>
        public Npc(string name, int id, Vector2f size, Vector2f startingPosition, Quest quest)
        {
            this.name = name;
            this.id = id;
            position = startingPosition;
            mask = new FloatRect(new Vector2f(position.X, position.Y), new Vector2f(size.X, size.Y));
            targetMask = Mask;
            currentAnimation = 11;
            velocity = new Vector2f(0, 0);
            standing = true;
            this.quest = quest;
            questAccepted = false;

            animations = new List<Anim>();
            animations.Add(new Anim(new Animation("player", 4, 30, 1), 11));
            animations.Add(new Anim(new Animation("player", 4, 30, 2), 12));
            animations.Add(new Anim(new Animation("player", 4, 30, 3), 13));
            animations.Add(new Anim(new Animation("player", 4, 30, 4), 14));
        }

        /// <summary>
        /// creates a bastard to play with
        /// </summary>
        /// <param name="name">name of the dude (derp)</param>
        /// <param name="id">id (not sure what for)</param>
        /// <param name="size">sets the size of the player-Rectangle</param>
        /// <param name="startingPosition">where the player is being creates</param>
        public Npc(string name, int id, Vector2f size, Vector2f startingPosition)
        {
            this.name = name;
            this.id = id;
            position = startingPosition;
            mask = new FloatRect(new Vector2f(position.X, position.Y), new Vector2f(size.X, size.Y));
            targetMask = Mask;
            currentAnimation = 11;
            velocity = new Vector2f(0, 0);
            standing = true;
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
        /// Returns a Vector at the Center of the players mask
        /// </summary>
        public Vector2f Position()
        {
            return new Vector2f(position.X + mask.Width / 2, position.Y + mask.Height / 2);
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
        }

        private void UpdateSprite()
        {
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

        public void Interact(Inventory inventory)
        {
            if(questAccepted)
            {
                if (QuestManager.QuestInList(quest.name))
                    QuestManager.CheckQuest(quest.name, inventory);
            }

            if (quest != null && !questAccepted)
            {
                QuestManager.currentQuests.Add(quest);
                questAccepted = true;
            }
        }

        /// <summary>
        /// draws the current animation of the player
        /// </summary>
        /// <param name="window">window of the main-program</param>
        public void Draw(RenderWindow window)
        {
            UpdateSprite();

            Anim.GetAnimID(animations, currentAnimation).Animation.Draw(window);

            //RectangleShape test = new RectangleShape(new Vector2f(examinationMask.Width, examinationMask.Height)); //TODO: Debug. remove once working
            //test.Position = new Vector2f(examinationMask.Left, examinationMask.Top);

            //window.Draw(test);
        }
    }
}

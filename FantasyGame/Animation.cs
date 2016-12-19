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
    class Animation
    {
        #region variables

        /// <summary>
        /// List of int, that memorize which tiles of the spritemap are in the animation
        /// </summary>
        private List<int> spriteIDs;
        private int spriteMapID;
        private Vector2f position;
        private int frameAmount, currentFrame, currentSprite;
        private float size;
        private bool animationLoop, animationPaused;

        public bool AnimationLoop

        {
            get
            {
                return animationLoop;
            }
            set
            {
                animationLoop = value;
            }
        }

        public Vector2f Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        public float Size
        {
            set
            {
                size = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates an Instanze of an Animation. Generally used in an List of Animations
        /// </summary>
        /// <param name="spriteMapName">name of the Spritemap it loads its sprites from</param>
        /// <param name="spriteAmount">How many Sprites in an horizontal order are used in the Animation Don't go higher than the Spritemap-width</param>
        /// <param name="frameAmount">Amount of Frames that have to pass by untill the animation is finished</param>
        /// <param name="column">Column in the Spritemap that has the needed sprites</param>
        public Animation(string spriteMapName, int spriteAmount, int frameAmount, int column)
        {
            this.frameAmount = frameAmount;

            spriteIDs = new List<int>();
            Size = 1;
            animationPaused = false;

            spriteMapID = 0;

            for (int r = 0; r < ContentManager.spriteMaps.Count; r++)
            {
                if (spriteMapName == ContentManager.spriteMaps[r].name)
                    spriteMapID = r;
            }

            for (int i = 0; i < spriteAmount; i++)
            {
                spriteIDs.Add(ContentManager.spriteMaps[spriteMapID].width * (column - 1) + i + 1);
            }

            animationLoop = true;
        }

        /// <summary>
        /// used to Reset by setting the Sprite and Framenumber back to 0
        /// </summary>
        public void ResetAnimation()
        {
            ContentManager.spriteMaps[spriteMapID].Sprites[spriteIDs[0]].Scale = new Vector2f(size, size);

            ContentManager.spriteMaps[spriteMapID].Sprites[spriteIDs[0]].Position = new Vector2f(position.X 
             /*- (ContentManager.spriteMaps[spriteMapID].Sprites[spriteIDs[0]].TextureRect.Width * size) / 2*/,
             position.Y/* - (ContentManager.spriteMaps[spriteMapID].Sprites[spriteIDs[0]].TextureRect.Height * size) / 2*/);

            currentFrame = 0;
            currentSprite = 0;
        }

        /// <summary>
        /// pauses the Animation, meaning that it freezes
        /// </summary>
        /// <param name="paused">bool that sets whether the Animation should freeze</param>
        public void PauseAnimation(bool paused)
        {
            animationPaused = paused;
        }

        /// <summary>
        /// Updates the Animation, recognizing which Frame should be drawn next
        /// </summary>
        private void CurrentSpriteManager()
        {
            if (currentFrame >= frameAmount / spriteIDs.Count)
            {
                currentSprite++;
                currentFrame = 0;
            }

            if (currentSprite + 1 > spriteIDs.Count && animationLoop)
            {
                currentSprite = 0;
            }

            if (currentSprite + 1 > spriteIDs.Count && !animationLoop)
            {
                currentSprite--;
            }
        }

        /// <summary>
        /// Updates  the Animation. Necessary to play the animation. If animation is paused, it doesn't Update
        /// </summary>
        public void Update()
        {
            if (!animationPaused)
            {
                CurrentSpriteManager();

                ContentManager.spriteMaps[spriteMapID].Sprites[spriteIDs[currentSprite]].Scale = new Vector2f(size, size);

                ContentManager.spriteMaps[spriteMapID].Sprites[spriteIDs[currentSprite]].Position = new Vector2f(position.X
                 /*- (ContentManager.spriteMaps[spriteMapID].Sprites[spriteIDs[currentSprite]].TextureRect.Width * size) / 2*/,
                 position.Y/* - (ContentManager.spriteMaps[spriteMapID].Sprites[spriteIDs[currentSprite]].TextureRect.Height * size) / 2*/);

                currentFrame++;
            }

        }

        /// <summary>
        /// Draws the current Sprite of the Animation.
        /// </summary>
        /// <param name="window">main-window of the program</param>
        public void Draw(RenderWindow window)
        {
            window.Draw(new Sprite(ContentManager.spriteMaps[spriteMapID].Sprites[spriteIDs[currentSprite]]));
        }

        /// <summary>
        /// Used to recognize, whether the Animation has ended. Recommended with Animationloop on false
        /// </summary>
        /// <returns>Returns a boolean at the last frame of the animation</returns>
        public bool EndOfAnimation()
        {
            return (currentFrame >= frameAmount / spriteIDs.Count && currentSprite + 1 == spriteIDs.Count);
        }

        #endregion
    }

    /// <summary>
    /// Basically an Animation with ID
    /// </summary>
    class Anim
    {
        /// <summary>
        /// Creates an Instance of an Anim.
        /// </summary>
        /// <param name="animation">Animations of the Object</param>
        /// <param name="id">ID. Durhur</param>
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

        public static Anim GetAnimID(List<Anim> animations, int id)
        {
            for (int i = 0; i < animations.Count; i++)
                if (animations[i].ID == id)
                    return animations[i];
            throw new ArgumentOutOfRangeException("No Animation with wanted ID exists");
        }
    }
}

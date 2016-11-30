using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Animation
{
    class Animation
    {
        #region variables

        private List<Sprite> sprites;
        private Vector2f position;
        private int frameAmount, currentFrame, currentSprite;
        private float size;
        private bool animationLoop;

        private bool AnimationLoop

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
            get
            {
                return size;
            }

            set
            {
                size = value;
            }
        }

        #endregion

        #region Methods

        public Animation(Texture spriteSheet, int spriteAmount, int frameAmount, int column, Vector2i spriteSize)
        {
            this.frameAmount = frameAmount;

            sprites = new List<Sprite>();
            Size = 1;

            for (int i = 0; i < spriteAmount; i++)

            {
                sprites.Add(new Sprite(spriteSheet ,new IntRect(new Vector2i((i * spriteSize.X), ((column - 1) * spriteSize.Y)), spriteSize)));
            }

            animationLoop = true;
        }

        /// <summary>
        /// used to Reset
        /// </summary>
        public void ResetAnimation()
        {
            currentFrame = 0;
            currentSprite = 0;
        }

        private void CurrentSpriteManager()
        {
            if (currentFrame >= frameAmount / sprites.Count)
            {
                currentSprite++;
                currentFrame = 0;
            }

            if (currentSprite + 1 > sprites.Count && animationLoop)
            {
                currentSprite = 0;
            }

            if (currentSprite + 1 > sprites.Count && !animationLoop)
            {
                currentSprite--;
            }
        }

        public void Update()
        {
            CurrentSpriteManager();

            sprites[currentSprite].Scale = new Vector2f(size, size);
            sprites[currentSprite].Position = new Vector2f(position.X - (sprites[currentSprite].TextureRect.Width * size) / 2 , position.Y - (sprites[currentSprite].TextureRect.Height * size) / 2);
            Console.WriteLine(sprites[currentSprite].TextureRect.Width);
            

            currentFrame++;
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(new Sprite(sprites[currentSprite]));
        }

        public bool EndOfAnimation()
        {
            return (currentFrame >= frameAmount / sprites.Count && currentSprite + 1 == sprites.Count);
        }

        #endregion
    }
}

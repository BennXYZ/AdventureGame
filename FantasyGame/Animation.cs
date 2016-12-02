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

        private List<int> spriteIDs;
        private int spriteMapID;
        private Vector2f position;
        private int frameAmount, currentFrame, currentSprite;
        private float size;
        private bool animationLoop;

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

        public Animation(string spriteMapName, int spriteAmount, int frameAmount, int column)
        {
            this.frameAmount = frameAmount;

            spriteIDs = new List<int>();
            Size = 1;

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
            currentFrame = 0;
            currentSprite = 0;
        }

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

        public void Update()
        {
            CurrentSpriteManager();

            ContentManager.spriteMaps[spriteMapID].Sprites[spriteIDs[currentSprite]].Scale = new Vector2f(size, size);

            ContentManager.spriteMaps[spriteMapID].Sprites[spriteIDs[currentSprite]].Position = new Vector2f(position.X - 
            (ContentManager.spriteMaps[spriteMapID].Sprites[spriteIDs[currentSprite]].TextureRect.Width * size) / 2 , 
            position.Y - (ContentManager.spriteMaps[spriteMapID].Sprites[spriteIDs[currentSprite]].TextureRect.Height * size) / 2);
            
            currentFrame++;
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(new Sprite(ContentManager.spriteMaps[spriteMapID].Sprites[spriteIDs[currentSprite]]));
        }

        public bool EndOfAnimation()
        {
            return (currentFrame >= frameAmount / spriteIDs.Count && currentSprite + 1 == spriteIDs.Count);
        }

        #endregion
    }
}

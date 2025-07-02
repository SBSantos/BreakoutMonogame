using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout.Manager
{
    public abstract class Sprite
    {
        /// <summary>
        /// Texture of the sprite.
        /// </summary>
        public Texture2D Texture { get; set; }

        /// <summary>
        /// Position of the sprite.
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// Rectangle of the sprite.
        /// </summary>
        public Rectangle Rectangle => new Rectangle((int)Position.X, (int)Position.Y, Texture.Width * SCALE, Texture.Height * SCALE);

        /// <summary>
        /// Stores the middle of the screen.
        /// </summary>
        public int MiddleScreen = Globals.ScreenResolution.X / 2;

        /// <summary>
        /// Stores the invisible left side wall of the game.
        /// </summary>
        public int LeftSideWall
        {
            get { return MiddleScreen / 2; }
        }

        /// <summary>
        /// Stores the invisible right side wall of the game.
        /// </summary>
        public int RightSideWall
        {
            get { return MiddleScreen + LeftSideWall; }
        }

        /// <summary>
        /// To amplify the sprite.
        /// </summary>
        protected const int SCALE = 2;

        public Sprite(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
        }

        public virtual void Draw()
        {
            Globals.SpriteBatch.Draw(Texture, Rectangle, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f);
        }

        public virtual void Update() { }
    }
}

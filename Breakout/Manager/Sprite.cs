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
        /// Set the sprite origin position in the middle of the texture, not on the left side.
        /// </summary>
        public Vector2 OriginPosition => new Vector2(Texture.Width, Texture.Height);

        /// <summary>
        /// Rectangle of the sprite.
        /// </summary>
        public Rectangle Rectangle => new Rectangle((int)(Position.X - OriginPosition.X), (int)(Position.Y - OriginPosition.Y), Texture.Width * SCALE, Texture.Height * SCALE);

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

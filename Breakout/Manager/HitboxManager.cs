using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout.Manager
{
    public class HitboxManager : IHitable
    {
        /// <summary>
        /// Texture of the hitbox.
        /// </summary>
        public Texture2D HitboxTexture { get; set; }

        /// <summary>
        /// The width of the hitbox rectangle.
        /// </summary>
        public int RectangleWidth { get; set; }

        /// <summary>
        /// The height of the hitbox rectangle.
        /// </summary>
        public int RectangleHeight { get; set; }

        /// <summary>
        /// The origin position of the rectangle width hitbox. Get the width of the rectangle and divide it by 2.
        /// </summary>
        public int RectangleWidthOriginPosition { get; set; }

        /// <summary>
        /// The origin position of the rectangle height hitbox. Get the height of the rectangle and divide it by 2.
        /// </summary>
        public int RectangleHeightOriginPosition { get; set; }

        public Rectangle HitboxRectangle { get; }
        public Vector2 Position { get; }

        public HitboxManager(Vector2 position, int rectangleWidth, int rectangleHeight, int scale)
        {
            HitboxTexture = new Texture2D(Globals.GraphicsDevice, 1, 1);
            HitboxTexture.SetData([new Color(Color.Red, 0.1f)]);

            Position = position;

            RectangleWidth = rectangleWidth * scale;
            RectangleHeight = rectangleHeight * scale;

            RectangleWidthOriginPosition = (rectangleWidth / 2) * scale;
            RectangleHeightOriginPosition = (rectangleHeight / 2) * scale;

            HitboxRectangle = new Rectangle((int)(Position.X - RectangleWidthOriginPosition), (int)(Position.Y - RectangleHeightOriginPosition), RectangleWidth, RectangleHeight);
        }
    }
}

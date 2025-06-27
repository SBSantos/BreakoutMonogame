using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout
{
    public class Globals
    {
        public static ContentManager Content { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }
        public static GraphicsDevice GraphicsDevice { get; set; }
        public static float Time { get; set; }
        public static Texture2D Texture { get; set; }
        public static GraphicsDeviceManager Graphics { get; set; }
        public static Point ScreenResolution;
        public static int MiddleScreen = ScreenResolution.X / 2;
        public static int LeftSideWall = MiddleScreen / 2;
        public static int RightSideWall = MiddleScreen + LeftSideWall;

        public static void Update(GameTime gameTime)
        {
            Time = (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public static void Resolution()
        {
            Graphics.PreferredBackBufferWidth = ScreenResolution.X;
            Graphics.PreferredBackBufferHeight = ScreenResolution.Y;
            Graphics.ApplyChanges();
        }

        public static void SetResolutionValues(int width, int height)
        {
            ScreenResolution.X = width;
            ScreenResolution.Y = height;
        }
    }
}

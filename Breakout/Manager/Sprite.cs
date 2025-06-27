using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout.Manager
{
    public class Sprite
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position;
        public Vector2 OriginPosition => new Vector2(Texture.Width / 2, Texture.Height / 2);
        public Rectangle Rectangle => new Rectangle((int)(Position.X - OriginPosition.X), (int)(Position.Y - OriginPosition.Y), Texture.Width * 2, Texture.Height * 2);
        public int MiddleScreen = Globals.ScreenResolution.X / 2;
        public int LeftSideWall
        {
            get { return MiddleScreen / 2; }
        }
        public int RightSideWall
        {
            get { return MiddleScreen + LeftSideWall; }
        }

        public Sprite(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
        }

        public virtual void Draw()
        {
            Globals.SpriteBatch.Draw(Texture, Rectangle, null, Color.White, 0f, OriginPosition, SpriteEffects.None, 1f);
        }

        public virtual void Update() { }
    }
}

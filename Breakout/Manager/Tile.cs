using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Breakout.Manager
{
    public class Tile : Sprite
    {
        private Rectangle Destination, Source;

        public Tile(Texture2D texture, Rectangle destination, Rectangle source, Vector2 position) : base(texture, position)
        {
            Texture = texture;
            Position = position;
            Destination = destination;
            Source = source;
        }

        public override void Draw()
        {
            Globals.SpriteBatch.Draw(Texture, Destination, Source, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);
        }
    }
}

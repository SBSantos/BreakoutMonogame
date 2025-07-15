using Breakout.Manager;
using Breakout.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout.Bricks
{
    public class Brick : Sprite, IHitable
    {
        public Texture2D BrickTexture;
        public bool Active { get; set; } = false;
        private readonly int _widthOffset = 20;
        private readonly int _heightOffset = 8;
        public Rectangle HitboxRectangle => new Rectangle((int)(Position.X + _widthOffset / 3), (int)(Position.Y + Texture.Height / 2 - _heightOffset / 2), _widthOffset, _heightOffset);
        private SoundEffect _brickSound;

        public Brick(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Texture = texture;
            Position = position;
        }

        public override void Draw()
        {
            if (!Active)
            {
                Globals.SpriteBatch.Draw(Texture, Rectangle, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.1f);

                // Brick hitbox
                //BrickTexture = new Texture2D(Globals.GraphicsDevice, 1, 1);
                //BrickTexture.SetData([new Color(Color.Red, 0.1f)]);
                //Globals.SpriteBatch.Draw(BrickTexture, HitboxRectangle, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.2f);
            }
        }
    }
}

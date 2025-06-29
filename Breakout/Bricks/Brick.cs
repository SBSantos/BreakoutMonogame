using Breakout.Manager;
using Breakout.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout.Bricks
{
    public abstract class Brick :  Sprite
    {
        /// <summary>
        /// The spritesheet texture of the bricks gets big asf if I use the Sprite class rectangle so I created this rectangle to make it smaller.
        /// </summary>
        public Rectangle BrickRectangle => new Rectangle((int)(Position.X - SrcRectangle.Width), (int)(Position.Y - SrcRectangle.Height), SrcRectangle.Width * SCALE, SrcRectangle.Height * SCALE);

        /// <summary>
        /// Source rectangle for a especified texture.
        /// </summary>
        public Rectangle SrcRectangle { get; set; }

        /// <summary>
        /// Hitbox of the brick.
        /// </summary>
        public HitboxManager HitboxManager => new(Position, 20, 8, SCALE);

        /// <summary>
        /// Sound.
        /// </summary>
        private SoundEffect _brickSound;

        public Brick(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Texture = texture;
            Position = position;
        }

        public virtual bool CheckBallCollision(Ball ball)
        {
            if (ball.HitboxManager.HitboxRectangle.Intersects(HitboxManager.HitboxRectangle))
            {
                //_brickSound.Play();
                ball.Direction.Y = -ball.Direction.Y;
                return true;
            }

            return false;
        }
    }
}

using Breakout.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Breakout.Bricks
{
    public class Bricks : DrawableGameComponent
    {
        public Vector2 Position { get; set; }
        public Rectangle Rect { get; set; }
        public Color Color { get; set; }
        public bool Active { get; set; } = true;
        private SoundEffect _brickSound;

        public Bricks(Game game, Vector2 position, Rectangle rect, Color color) : base(game)
        {
            Position = position;
            Rect = rect;
            Color = color;

            //_brickSound = Globals.Content.Load<SoundEffect>("Brick_Sound");
        }
        /*
        public bool CheckBallCollision(Ball ball)
        {
            if (Active && ball.Rect.Intersects(Rect))
            {
                _brickSound.Play();
                ball.Direction.Y = -ball.Direction.Y;
                return true;
            }

            return false;
        }*/

        public override void Draw(GameTime gameTime)
        {
            if (Active)
            {
                Globals.SpriteBatch.Begin();
                Globals.SpriteBatch.Draw(Globals.Texture, Rect, Color);
                Globals.SpriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}

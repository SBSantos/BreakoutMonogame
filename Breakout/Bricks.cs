using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Breakout
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
            this.Position = position;
            this.Rect = rect;
            this.Color = color;

            this._brickSound = Globals.Content.Load<SoundEffect>("Brick_Sound");
        }

        public bool CheckBallCollision(Ball ball)
        {
            if (Active && ball.Rect.Intersects(Rect))
            {
                _brickSound.Play();
                ball.Direction.Y = -ball.Direction.Y;
                return true;
            }

            return false;
        }

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

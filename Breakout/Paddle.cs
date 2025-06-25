using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class Paddle
    {
        public Vector2 Position;
        public Rectangle Rect { get; private set; }
        public float Speed = 400f;
        private readonly Keys _left;
        private readonly Keys _right;
        private SoundEffect _paddleSound;

        public Paddle(Vector2 position, Keys left, Keys right)
        {
            this.Position = position;
            this._left = left;
            this._right = right;

            this._paddleSound = Globals.Content.Load<SoundEffect>("Paddle_Sound");
        }

        public void CheckPaddleBallCollision(Ball ball)
        {
            if (ball.Rect.Intersects(Rect))
            {
                _paddleSound.Play();
                ball.Direction.Y *= -ball.Direction.Y;
            }
        }

        public void Update()
        {
            if (InputManager.IsKeyDown(_left)) { Position.X -= Speed * Globals.Time; }
            if (InputManager.IsKeyDown(_right)) { Position.X += Speed * Globals.Time; }

            Position.X = MathHelper.Clamp(Position.X, 0, Globals.WIDTH - 60);

            Rect = new((int)Position.X, (int)Position.Y + 350, 60, 8);
        }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(Globals.Texture, Rect, Color.Turquoise);
        }
    }
}

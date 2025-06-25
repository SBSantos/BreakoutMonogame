using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class Ball
    {
        public Vector2 Position;
        public Rectangle Rect { get; private set; }
        public float Speed = 300f;
        public bool Run { get; set; } = false;
        public Keys Space { get; set; }
        public Vector2 Direction;

        public Ball(Vector2 position)
        {
            this.Position = position;
            ResetBallDirection(Position);
        }

        public void ResetBallDirection(Vector2 p)
        {
            Random r = new();
            Position = p;

            do
            {
                Direction.X = r.Next(2) == 0 ? 1 : -1;
            } while (Direction.X == 0);

            do
            {
                Direction.Y = r.Next(2) == 0 ? 1 : -1;
            } while (Direction.Y == 0);

            Speed = 300f;
        }

        public void CheckBallCollision()
        {
            if (Position.X > Globals.WIDTH - 10 || Position.X < 10)
            {
                Direction.X = -Direction.X;
            }
            if (Position.Y < -0)
            {
                Direction.Y = -Direction.Y;
            }
        }

        public void Update()
        {
            Rect = new((int)Position.X - 5, (int)Position.Y, 10, 10);

            if (Run)
            {
                Position.X += Direction.X * Speed * Globals.Time;
                Position.Y += Direction.Y * Speed * Globals.Time;

                CheckBallCollision();
            }
            else
            {
                Position.X = MathHelper.Clamp(Position.X, 30, Globals.WIDTH - 30);

                if (InputManager.IsKeyDown(Keys.D)) { Position.X += 400f * Globals.Time; }
                if (InputManager.IsKeyDown(Keys.A)) { Position.X -= 400f * Globals.Time; }
            }
        }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(Globals.Texture, Rect, Color.White);
        }
    }
}

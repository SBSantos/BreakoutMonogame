using System;
using Breakout.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout.Player
{
    public class Ball : Sprite
    {
        public float MoveSpeed;
        public bool Run { get; set; } = false;
        public int Lives { get; set; }
        public Vector2 Direction;
        public float RotationSpeed { get; set; }
        private const int OFFSET = 4;

        public Ball(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Texture = Globals.Content.Load<Texture2D>("Textures/Ball");
            Position = position;
            Lives = 3;
            ResetBallDirection(Position);
        }

        public override void Draw()
        {
            
            if (Run) 
            { 
                // Pos X and Y += Texture divided by 2 + Ball sprite + offset (12 + 4);
                // Now it's working 100%, for sure! :D
                Globals.SpriteBatch.Draw(Texture, new Rectangle((int)Position.X + (Texture.Width / 2) + 16, (int)Position.Y + (Texture.Height / 2) + 16, Texture.Width, Texture.Height), null, Color.White, RotationSpeed, new Vector2(Texture.Width / 2, Texture.Height / 2), SpriteEffects.None, 0.4f); 
            }
            else { Globals.SpriteBatch.Draw(Texture, new Rectangle((int)Position.X + 16, (int)Position.Y + 16, Texture.Width, Texture.Height), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.4f); }
        }

        public override void Update()
        {
            if (Run)
            {
                MoveSpeed = 160f;
                RotationSpeed += 0.3f;
                Position.X += Direction.X * MoveSpeed * Globals.Time;
                Position.Y -= Direction.Y * MoveSpeed * Globals.Time;
            }
            else
            {
                if (InputManager.IsKeyDown(Keys.D) || InputManager.IsKeyDown(Keys.Right)) { Position.X += MoveSpeed * Globals.Time; }
                if (InputManager.IsKeyDown(Keys.A) || InputManager.IsKeyDown(Keys.Left)) { Position.X -= MoveSpeed * Globals.Time; }

                Position = Vector2.Clamp(Position, new Vector2(LeftSideWall - Texture.Width / 2, 0), new Vector2(RightSideWall - 48, (Globals.ScreenResolution.Y / 2) * 1.7f));
            }
        }

        
        public void ResetBallDirection(Vector2 p)
        {
            Random r = new();
            Position = p;

            do
            {
                Direction.X = r.Next(2) == 0 ? 0.8f : -0.8f;
            } while (Direction.X == 0);

            Direction.Y = 1;
        }

        public void CheckWallCollision(Ball ball)
        {
            var newRect = ball.CalculateBound(ball.Position);
            if (newRect.Right >= ball.RightSideWall || newRect.Left <= ball.LeftSideWall)
            {
                ball.Direction.X = -ball.Direction.X;
            }
            if (ball.Position.Y < -32)
            {
                ball.Direction.Y = -ball.Direction.Y;
            }
        }

        public Rectangle CalculateBound(Vector2 pos)
        {
            return new Rectangle((int)(pos.X + Texture.Width - OFFSET / 2), (int)(pos.Y + Texture.Height - OFFSET / 2), OFFSET, OFFSET);
        }

        public bool CheckBallOutsideOfTheScreen()
        {
            if (Position.Y > Globals.ScreenResolution.Y) 
            {
                Lives--;
                return true;
            }
            return false;
        }

        public bool CheckDefeat(GameManager gameManager)
        {
            if (Lives == 0) { return gameManager.Defeat = true; }
            return false;
        }
    }
}

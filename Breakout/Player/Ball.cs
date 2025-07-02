using System;
using Breakout.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Breakout.Bricks;
using System.Reflection.Metadata;

namespace Breakout.Player
{
    public class Ball : Sprite
    {
        public float MoveSpeed;
        public bool Run { get; set; } = false;
        public bool RunTimer;
        public int Score { get; set; }
        public int Lives { get; set; }
        public int NumberBricksBroken { get; set; }
        public Vector2 Direction;
        private float RotationSpeed { get; set; }
        private const int OFFSET = 4;

        public Ball(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Texture = Globals.Content.Load<Texture2D>("Textures/Ball");
            Position = position;
            RunTimer = false;
            Score = 0;
            Lives = 3;
            NumberBricksBroken = 0;
            ResetBallDirection(Position);
        }

        public override void Draw()
        {
            
            if (Run) 
            { 
                // Same shit but removing the origin position in the X and Y position of the rectangle and setting the vector2 origin parameter in the middle.
                // Now the rotation is working! :D
                // Update: Not working anymore :(

                Globals.SpriteBatch.Draw(Texture, Rectangle, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f); 
            }
            else { Globals.SpriteBatch.Draw(Texture, Rectangle, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f); }
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
                if (InputManager.IsKeyDown(Keys.D) && Position.X < RightSideWall - Texture.Width * SCALE) { Position.X += MoveSpeed * Globals.Time; }
                if (InputManager.IsKeyDown(Keys.A) && Position.X > LeftSideWall) { Position.X -= MoveSpeed * Globals.Time; }
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
            if (ball.Position.Y < 0)
            {
                ball.Direction.Y = -ball.Direction.Y;
            }
        }

        public Rectangle CalculateBound(Vector2 pos)
        {
            return new Rectangle((int)(pos.X + Texture.Width - OFFSET), (int)(pos.Y + Texture.Height - OFFSET), OFFSET * SCALE, OFFSET * SCALE);
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

        public void NewLive(Paddle paddle, Ball ball, GameManager gameManager)
        {
            ball.Run = false;
            paddle.Position = new(gameManager.MiddleX - gameManager.TextureOffset, (Globals.ScreenResolution.Y / 2) * 1.7f);

            if (gameManager.Defeat)
            {

            }
            else
            {
                var ballYPos = paddle.Position.Y - (paddle.Texture.Height / 3) - 2;
                ball.ResetBallDirection(new(gameManager.MiddleX - gameManager.TextureOffset, ballYPos));
            }
        }

        public bool CheckDefeat(GameManager gameManager)
        {
            if (Lives == 0) { return gameManager.Defeat = true; }
            return false;
        }
    }
}

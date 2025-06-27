using System;
using Breakout.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout.Player
{
    public class Ball : Sprite
    {
        private readonly float _moveSpeed = 210f;
        private readonly float _idleSpeed = 300f;
        public bool Run { get; set; } = false;
        public Vector2 Direction;
        private float RotationSpeed { get; set; }

        public Ball(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Texture = Globals.Content.Load<Texture2D>("Textures/Ball");
            Position = position;
            ResetBallDirection(Position);
        }

        public override void Draw()
        {
            if (Run) { Globals.SpriteBatch.Draw(Texture, Rectangle, null, Color.White, RotationSpeed, OriginPosition, SpriteEffects.None, 1f); }
            else { Globals.SpriteBatch.Draw(Texture, Rectangle, null, Color.White, 0f, OriginPosition, SpriteEffects.None, 1f); }
        }

        public override void Update()
        {
            if (Run)
            {
                RotationSpeed += 0.3f;
                Position.X += Direction.X * _moveSpeed * Globals.Time;
                Position.Y -= Direction.Y * _moveSpeed * Globals.Time;
                CheckBallCollision();
            }
            else
            {
                if (InputManager.IsKeyDown(Keys.D) && Position.X < RightSideWall) { Position.X += _idleSpeed * Globals.Time; }
                if (InputManager.IsKeyDown(Keys.A) && Position.X > LeftSideWall) { Position.X -= _idleSpeed * Globals.Time; }
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
        
        public void CheckBallCollision()
        {
            if (Position.X >= RightSideWall || Position.X <= LeftSideWall) 
            {
                Direction.X = -Direction.X;
            }
            if (Position.Y < 0)
            {
                Direction.Y = -Direction.Y;
            }
        }
    }
}

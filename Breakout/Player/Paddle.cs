using Breakout.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Breakout.Player
{
    public class Paddle : Sprite
    {
        private float _speed = 300f;
        private SoundEffect _paddleSound;

        public Paddle(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Texture = Globals.Content.Load<Texture2D>("Textures/Paddle");
            Position = position;
            //_paddleSound = Globals.Content.Load<SoundEffect>("Paddle_Sound");
        }
        
        public void CheckPaddleBallCollision(Ball ball)
        {
            if (ball.Rectangle.Intersects(Rectangle))
            {
                //_paddleSound.Play();
                ball.Direction.Y *= -ball.Direction.Y;
            }
        }

        public override void Draw()
        {
            base.Draw();
        }

        public override void Update()
        {
            if (InputManager.IsKeyDown(Keys.A) && Position.X > LeftSideWall) { Position.X -= _speed * Globals.Time; }
            if (InputManager.IsKeyDown(Keys.D) && Position.X < RightSideWall) { Position.X += _speed * Globals.Time; }
        }
    }
}

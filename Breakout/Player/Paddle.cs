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
        private readonly float _speed = 300f;
        private readonly SoundEffect _paddleSound;
        public HitboxManager HitboxManager => new(Position, Texture.Width, 6, SCALE);

        public Paddle(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Texture = Globals.Content.Load<Texture2D>("Textures/Paddle");
            Position = position;
            //_paddleSound = Globals.Content.Load<SoundEffect>("Paddle_Sound");
        }
        
        public void CheckPaddleBallCollision(Ball ball)
        {
            if (ball.HitboxManager.HitboxRectangle.Intersects(HitboxManager.HitboxRectangle))
            {
                //_paddleSound.Play();
                ball.Direction.Y = -ball.Direction.Y;
            }
        }

        public override void Draw()
        {
            base.Draw();

            // Paddle hitbox.
            //Globals.SpriteBatch.Draw(HitboxManager.HitboxTexture, HitboxManager.HitboxRectangle, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f);
        }

        public override void Update()
        {
            if (InputManager.IsKeyDown(Keys.A) && HitboxManager.HitboxRectangle.Left > LeftSideWall)
            { 
                Position.X -= _speed * Globals.Time;
            }
            if (InputManager.IsKeyDown(Keys.D) && HitboxManager.HitboxRectangle.Right < RightSideWall)
            { 
                Position.X += _speed * Globals.Time; 
            }
        }
    }
}

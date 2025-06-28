using Breakout.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Breakout.Player
{
    public class Paddle : Sprite, IHitable
    {
        private float _speed = 300f;
        private SoundEffect _paddleSound;
        private HitboxManager _hitboxManager => new(Texture.Width, 6, SCALE);
        public Rectangle HitboxRectangle => new Rectangle
            ((int)(Position.X - _hitboxManager.RectangleWidthOriginPosition), 
            (int)(Position.Y - _hitboxManager.RectangleHeightOriginPosition), 
            _hitboxManager.RectangleWidth, 
            _hitboxManager.RectangleHeight);

        public Paddle(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Texture = Globals.Content.Load<Texture2D>("Textures/Paddle");
            Position = position;
            //_paddleSound = Globals.Content.Load<SoundEffect>("Paddle_Sound");
        }
        
        public void CheckPaddleBallCollision(Ball ball)
        {
            if (ball.HitboxRectangle.Intersects(HitboxRectangle))
            {
                //_paddleSound.Play();
                ball.Direction.Y = -ball.Direction.Y;
            }
        }

        public override void Draw()
        {
            base.Draw();

            // Paddle hitbox.
            //Globals.SpriteBatch.Draw(_hitboxManager.HitboxTexture, HitboxRectangle, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f);
        }

        public override void Update()
        {
            if (InputManager.IsKeyDown(Keys.A) && HitboxRectangle.Left > LeftSideWall)
            { 
                Position.X -= _speed * Globals.Time;
            }
            if (InputManager.IsKeyDown(Keys.D) && HitboxRectangle.Right < RightSideWall)
            { 
                Position.X += _speed * Globals.Time; 
            }
        }
    }
}

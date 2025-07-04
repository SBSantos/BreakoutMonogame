using Breakout.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout.Player
{
    public class Paddle : Sprite, IHitable
    {
        public float Speed = 300f;
        private readonly SoundEffect _paddleSound;
        private readonly int HeightOffset = 12;
        public Rectangle HitboxRectangle => new Rectangle((int)Position.X, (int)Position.Y + Texture.Height - HeightOffset / 2, Texture.Width * 2, HeightOffset);

        public Paddle(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Texture = Globals.Content.Load<Texture2D>("Textures/Paddle");
            Position = position;
            //_paddleSound = Globals.Content.Load<SoundEffect>("Paddle_Sound");
        }
        
        public void CheckPaddleBallCollision(Ball ball)
        {
            var newRect = ball.CalculateBound(ball.Position);
            if (newRect.Intersects(HitboxRectangle))
            {
                //_paddleSound.Play();
                ball.Direction.Y = -ball.Direction.Y;
            }
        }

        public override void Draw()
        {
            base.Draw();

            // Paddle hitbox.
            //var Texture = new Texture2D(Globals.GraphicsDevice, 1, 1);
            //Texture.SetData([new Color(Color.Red, 0.1f)]);
            //Globals.SpriteBatch.Draw(Texture, HitboxRectangle, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f);
        }

        public override void Update()
        {
            if (InputManager.IsKeyDown(Keys.A) || InputManager.IsKeyDown(Keys.Left))
            { 
                Position.X -= Speed * Globals.Time;
            }
            if (InputManager.IsKeyDown(Keys.D) || InputManager.IsKeyDown(Keys.Right))
            { 
                Position.X += Speed * Globals.Time; 
            }

            Position = Vector2.Clamp(Position, new Vector2(LeftSideWall, 0), new(RightSideWall - Texture.Width * SCALE, (Globals.ScreenResolution.Y / 2) * 1.7f));
        }
    }
}

using Breakout.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout.Manager
{
    class GameManager
    {
        private Texture2D Texture { get; }
        private readonly Paddle _paddle;
        private readonly Ball _ball;

        public GameManager()
        {
            Globals.SetResolutionValues(640, 480);

            _paddle = new(Texture, new Vector2(Globals.ScreenResolution.X / 2, (Globals.ScreenResolution.Y / 2) * 1.8f));

            var ballYPos = _paddle.Position.Y - (_paddle.Texture.Height / 3) - 32;
            _ball = new(Texture, new Vector2(Globals.ScreenResolution.X / 2, ballYPos));
        }

        public void Draw()
        {
            Globals.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _paddle.Draw();
            _ball.Draw();
            Globals.SpriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            Globals.Update(gameTime);

            InputManager.Update();
            _paddle.Update();
            _paddle.CheckPaddleBallCollision(_ball);
            _ball.Update();

            if (InputManager.SpacePressed && !_ball.Run) { _ball.Run = true; }

            if (InputManager.RPressed) { Reset(); }
        }

        public void Reset()
        {
            var ballYPos = _paddle.Position.Y - (_paddle.Texture.Height / 3) - 2;
            _paddle.Position = new(Globals.ScreenResolution.X / 2, (Globals.ScreenResolution.Y / 2) * 1.8f);
            _ball.ResetBallDirection(new(Globals.ScreenResolution.X / 2, ballYPos));
            //_paddle.Speed = 400f;
            //_ball.ResetBallDirection(new(Globals.WIDTH / 2, (Globals.HEIGHT / 2) + 340));
            //_ball.Speed = 300f;
            //_time = 0;
            //_score = 0;
            //_victory = false;
            //_defeat = false;
            //_bricks = new List<Bricks>();
            //_numBricksBroke = 0;
            //ListBricks();
        }
    }
}

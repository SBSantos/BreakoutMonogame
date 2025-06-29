using Breakout.Player;
using Breakout.Bricks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Breakout.Manager
{
    class GameManager
    {
        private Texture2D Texture { get; }
        private readonly Paddle _paddle;
        private readonly Ball _ball;
        private readonly HashSet<Brick> _brick;
        private readonly Rectangle[] _line = new Rectangle[3];
        private Texture2D _lineTexture;

        public GameManager()
        {
            Globals.SetResolutionValues(640, 480);

            _paddle = new(Texture, new Vector2(Globals.ScreenResolution.X / 2, (Globals.ScreenResolution.Y / 2) * 1.8f));

            var ballYPos = _paddle.Position.Y - (_paddle.Texture.Height / 3) - 2;
            _ball = new(Texture, new Vector2(Globals.ScreenResolution.X / 2, ballYPos));

            _brick = new();
            SetBrick();

            // Line that draw the middle, left and right walls.
            var middle = Globals.ScreenResolution.X / 2;
            var left = middle / 2;
            var right = left + middle;
            _line[0] = new Rectangle(middle, 0, 1, 1000);
            _line[1] = new Rectangle(left, 0, 1, 1000);
            _line[2] = new Rectangle(right, 0, 1, 1000);
        }

        public void Draw()
        {
            Globals.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _paddle.Draw();
            _ball.Draw();
            
            foreach (var brick in _brick) { brick.Draw(); }

            _lineTexture = new Texture2D(Globals.GraphicsDevice, 1, 1);
            _lineTexture.SetData([Color.White]);
            //Globals.SpriteBatch.Draw(_lineTexture, _line[0], Color.White);
            Globals.SpriteBatch.Draw(_lineTexture, _line[1], Color.White);
            Globals.SpriteBatch.Draw(_lineTexture, _line[2], Color.White);

            Globals.SpriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            Globals.Update(gameTime);

            InputManager.Update();
            _paddle.Update();
            _ball.Update();
            _paddle.CheckPaddleBallCollision(_ball);
            
            foreach (var brick in _brick)
            {
                if (brick.CheckBallCollision(_ball))
                {
                    _brick.Remove(brick);
                }
            }

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

        public void AddBrick(Brick brick)
        {
            _brick.Add(brick);
        }

        public void SetBrick()
        {
            var middle = Globals.ScreenResolution.X / 2;
            var left = middle / 2;
            var right = left + middle;

            // Yelow Bricks
            for (int i = 0; i < 8; i++) { AddBrick(new YellowBrick(Texture, new Vector2((left / 4 * i) + (left + 20), 50))); }

            //
        }
    }
}

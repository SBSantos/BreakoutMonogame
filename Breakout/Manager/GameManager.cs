using Breakout.Player;
using Breakout.Bricks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Breakout.Manager
{
    public class GameManager
    {
        private Texture2D Texture { get; }
        private Texture2D _lineTexture;
        private readonly Paddle _paddle;
        private readonly Ball _ball;
        private readonly Map _map;
        private readonly SpriteFont _font;
        private float _time;
        private float _seconds = 0f;
        private float _minutes = 0f;
        public bool Victory { get; set; }
        public bool Defeat { get; set; }
        private readonly Rectangle[] _line = new Rectangle[3];
        public int MiddleX => Globals.ScreenResolution.X / 2;
        public int LeftX => MiddleX / 2;
        public int RightX => LeftX + MiddleX;
        public int TextureOffset = 32;

        public GameManager()
        {
            Globals.SetResolutionValues(640, 480);
            _map = new();

            _paddle = new(Texture, new Vector2(MiddleX - TextureOffset, (Globals.ScreenResolution.Y / 2) * 1.7f));

            var ballYPos = _paddle.Position.Y - (_paddle.Texture.Height / 3) - 2;
            _ball = new(Texture, new Vector2(MiddleX - TextureOffset, ballYPos));

            _font = Globals.Content.Load<SpriteFont>("Font/Font");

            Victory = false;
            Defeat = false;

            _line[0] = new(LeftX, 0, 1, 1000);
            _line[1] = new(MiddleX, 0, 1, 1000);
            _line[2] = new(RightX, 0, 1, 1000);
        }

        public void Draw()
        {
            Globals.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _paddle.Draw();

            if (!Defeat) { _ball.Draw(); }

            _map.Draw();

            ShowGameInterface();
            GameResult();
            
            _lineTexture = new Texture2D(Globals.GraphicsDevice, 1, 1);
            _lineTexture.SetData([Color.White]);
            Globals.SpriteBatch.Draw(_lineTexture, _line[0], Color.White);
            //Globals.SpriteBatch.Draw(_lineTexture, _line[1], Color.White);
            Globals.SpriteBatch.Draw(_lineTexture, _line[2], Color.White);
            Globals.SpriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            Globals.Update(gameTime);

            InputManager.Update();
            _paddle.Update();
            _ball.Update();
            _ball.CheckWallCollision(_ball);
            _paddle.CheckPaddleBallCollision(_ball);
            _map.CheckBrickCollision(_ball);

            if (_ball.CheckBallOutsideOfTheScreen()) { _ball.NewLive(_paddle, this); }

            if (InputManager.SpacePressed && !_ball.Run) 
            { 
                _ball.Run = true;
                _ball.RunTimer = true;
            }
            else { _ball.MoveSpeed = 300f; }

            if (_map.CheckWin(this) || _ball.CheckDefeat(this))
            {
                _paddle.Speed = 0f;
                _ball.MoveSpeed = 0f;
                _ball.RotationSpeed = 0f;
                _ball.Direction.X = 0f;
                _ball.Direction.Y = 0f;
                _ball.RunTimer = false;

                if (InputManager.RPressed) { Reset(); }
            }
        }

        public void Reset()
        {
            var ballYPos = _paddle.Position.Y - (_paddle.Texture.Height / 3) - 2;
            _paddle.Position = new(MiddleX - TextureOffset, (Globals.ScreenResolution.Y / 2) * 1.7f);
            _ball.ResetBallDirection(new(MiddleX - TextureOffset, ballYPos));
            _paddle.Speed = 300f;
            _ball.MoveSpeed = 300f;
            _time = 0;
            _seconds = 0f;
            _minutes = 0f;
            _ball.Score = 0;
            _ball.NumberBricksBroken = 0;
            _ball.Run = false;
            _ball.Lives = 3;
            Victory = false;
            Defeat = false;
            _map.ResetBrick();
        }

        public string GameScore()
        {
            return string.Format("Score:{0}", _ball.Score);
        }

        public string Timer()
        {
            if (_ball.RunTimer)
            {
                _time += Globals.Time;
                _seconds = (float)Math.Floor(_time % 60);
                _minutes = (float)Math.Floor(_time / 60);
                return string.Format("Time:{0:00}:{1:00}", _minutes, _seconds);
            }
            return string.Format("Time:{0:00}:{1:00}", _minutes, _seconds);
        }

        public SpriteBatch ShowGameInterface()
        {
            Globals.SpriteBatch.DrawString(_font, GameScore(), new Vector2(LeftX + 10, 4), Color.White);
            Globals.SpriteBatch.DrawString(_font, Timer(), new Vector2(MiddleX - (MiddleX / 4) + 24, Globals.ScreenResolution.Y / 2), Color.White);
            Globals.SpriteBatch.DrawString(_font, "Lives:", new Vector2(MiddleX + 10, 4), Color.White);

            for (int i = 0; i < _ball.Lives; i++)
            {
                var Size = 24 * i;
                Globals.SpriteBatch.Draw(_ball.Texture, new Rectangle((MiddleX + LeftX / 2) + Size - 22, -22, _ball.Texture.Width * 2, _ball.Texture.Height * 2), Color.White);
            }
            return null;
        }

        public SpriteBatch GameResult()
        {
            var newFont = _font;
            if (_map.CheckWin(this))
            {
                Globals.SpriteBatch.DrawString(_font, "You Win!", new Vector2(MiddleX - (MiddleX / 4) + 38, (Globals.ScreenResolution.Y / 2) + 20), Color.Green);
            }
            if (_ball.CheckDefeat(this))
            {
                Globals.SpriteBatch.DrawString(_font, "You Lose!", new Vector2(MiddleX - (MiddleX / 4) + 32, (Globals.ScreenResolution.Y / 2) + 20), Color.Red);
            }
            if (_map.CheckWin(this) || _ball.CheckDefeat(this))
            {
                Globals.SpriteBatch.DrawString(_font, "Press R to restart the game", new Vector2(MiddleX - LeftX + 18, (Globals.ScreenResolution.Y / 2) + 40), Color.White);
            }
            return null;
        }
    }
}

using Breakout.Player;
using Breakout.Bricks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Globalization;
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

            Globals.SpriteBatch.DrawString(_font, GameScore(_ball), new Vector2(LeftX + 10, 0), Color.White);
            Globals.SpriteBatch.DrawString(_font, Timer(_ball), new Vector2(MiddleX - (MiddleX / 4) + 16, Globals.ScreenResolution.Y / 2), Color.White);

            ShowLife(_ball);

            if (_map.CheckWin(this))
            {
                Globals.SpriteBatch.DrawString(_font, "You Win!", new Vector2(MiddleX - (MiddleX / 4) + 28, (Globals.ScreenResolution.Y / 2) + 20), Color.Green);
            }
            if (_ball.CheckDefeat(this))
            {
                Globals.SpriteBatch.DrawString(_font, "You Lose!", new Vector2(MiddleX - (MiddleX / 4) + 28, (Globals.ScreenResolution.Y / 2) + 20), Color.Red);
            }

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

            if (_ball.CheckBallOutsideOfTheScreen()) { _ball.NewLive(_paddle, _ball, this); }

            //_ball.CheckPlayerLives(this);

            if (InputManager.SpacePressed && !_ball.Run) 
            { 
                _ball.Run = true;
                _ball.RunTimer = true;
            }
            else { _ball.MoveSpeed = 300f; }

            _map.CheckWin(this);
            _ball.CheckDefeat(this);
            if (Victory || Defeat)
            {
                _paddle.Speed = 0f;
                _ball.MoveSpeed = 0f;
                _ball.Direction.X = 0f;
                _ball.Direction.Y = 0f;

                if (InputManager.RPressed) 
                { 
                    Reset(_ball, _map);
                }
            }
        }

        public void Reset(Ball ball, Map map)
        {
            var ballYPos = _paddle.Position.Y - (_paddle.Texture.Height / 3) - 2;
            _paddle.Position = new(MiddleX - TextureOffset, (Globals.ScreenResolution.Y / 2) * 1.7f);
            _ball.ResetBallDirection(new(MiddleX - TextureOffset, ballYPos));
            _paddle.Speed = 300f;
            _ball.MoveSpeed = 300f;
            _time = 0;
            ball.Score = 0;
            ball.NumberBricksBroken = 0;
            Victory = false;
            Defeat = false;
            //_bricks = new List<Bricks>();
            //ListBricks();
        }

        public string GameScore(Ball ball)
        {
            return string.Format("Score:{0}", ball.Score);
        }

        public string Timer(Ball ball)
        {
            var second = 0f;
            var minutes = 0f;

            if (ball.RunTimer)
            {
                _time += Globals.Time;
                second = (float)Math.Floor(_time % 60);
                minutes = (float)Math.Floor(_time / 60);
                return string.Format("Time:{0:00}:{1:00}", minutes, second);
            }
            return string.Format("Time:{0:00}:{1:00}", minutes, second);
        }

        public SpriteBatch ShowLife(Ball ball)
        {
            Globals.SpriteBatch.DrawString(_font, "Lives:", new Vector2(MiddleX + 10, 0), Color.White);
            for (int i = 0; i < ball.Lives; i++)
            {
                var Size = 24 * i;
                Globals.SpriteBatch.Draw(ball.Texture, new Rectangle((MiddleX + LeftX / 2) + Size - 16, -24, ball.Texture.Width * 2, ball.Texture.Height * 2), Color.White);
            }
            return null;
        }
    }
}

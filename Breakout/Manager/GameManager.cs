using Breakout.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout.Manager
{
    public class GameManager
    {
        private readonly Paddle _paddle;
        private readonly Ball _ball;
        private readonly Map _map;
        private readonly SpriteFont _font;
        private Score _score;
        private Timer _timer;
        public bool Victory;
        public bool Defeat;

        public const int TEXTURE_SIZE = 32;

        private Texture2D Texture { get; }
        public int MiddleX => Globals.ScreenResolution.X / 2;
        public int LeftX => MiddleX / 2;
        public int RightX => LeftX + MiddleX;

        public GameManager()
        {
            Globals.SetResolutionValues(640, 480);
            _map = new();

            _paddle = new(Texture, new Vector2(MiddleX - TEXTURE_SIZE / 2, (Globals.ScreenResolution.Y / 2) * 1.7f));

            var ballYPos = _paddle.Position.Y - _paddle.Texture.Height / 2 - 7;
            _ball = new(Texture, new Vector2(MiddleX - TEXTURE_SIZE, ballYPos));

            _font = Globals.Content.Load<SpriteFont>("Font/Font");

            _score = new(_font);
            _timer = new(_font);

            Victory = false;
            Defeat = false;
        }

        public void Draw()
        {
            _paddle.Draw();

            if (!Defeat) { _ball.Draw(); }

            _score.DrawScore();
            _timer.DrawTimer();
            _map.Draw();

            ShowLives();
            GameResult();
        }

        public void Update(GameTime gameTime)
        {
            Globals.Update(gameTime);

            InputManager.Update();
            _paddle.Update();

            _ball.Update();
            _ball.CheckWallCollision(_ball);

            _paddle.CheckPaddleBallCollision(_ball);

            _map.CheckBrickCollision(_ball, _score);

            if (_ball.CheckBallOutsideOfTheScreen()) { ResetBallPaddlePosition(); }

            if (InputManager.SpacePressed && !_ball.Run) 
            { 
                _ball.Run = true;
                _timer.Run = true;
            }
            else { _ball.MoveSpeed = 300f; }

            if (_map.CheckWin(this) || _ball.CheckDefeat(this))
            {
                _paddle.Speed = 0f;
                _ball.MoveSpeed = 0f;
                _ball.RotationSpeed = 0f;
                _ball.Direction.X = 0f;
                _ball.Direction.Y = 0f;
                _timer.Run = false;

                if (InputManager.RPressed) { Reset(); }
            }
        }

        public void Reset()
        {
            ResetBallPaddlePosition();
            _paddle.Speed = 300f;
            _ball.MoveSpeed = 300f;
            _ball.Lives = 3;

            _timer = new(_font);
            _score = new(_font);

            Victory = false;
            Defeat = false;

            _map.ResetBrick();
        }

        public void ShowLives()
        {
            Globals.SpriteBatch.DrawString(_font, "Lives:", new Vector2(RightX + TEXTURE_SIZE + TEXTURE_SIZE / 2, TEXTURE_SIZE + TEXTURE_SIZE / 2), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.4f);

            for (int i = _ball.Lives; i > 0; i--)
            {
                var spacing = 32 * i;
                Globals.SpriteBatch.Draw(_ball.Texture, new Rectangle(RightX + TEXTURE_SIZE / 2 + spacing - TEXTURE_SIZE, TEXTURE_SIZE + TEXTURE_SIZE / 2, _ball.Texture.Width * 2, _ball.Texture.Height * 2), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.4f);
            }
        }

        public void GameResult()
        {
            int tileRow = 7;
            int yOffset = 4;
            if (_map.CheckWin(this))
            {
                int xOffset = 38;
                Globals.SpriteBatch.DrawString(_font, "You Win!", new Vector2(MiddleX - (MiddleX / 4) + xOffset, TEXTURE_SIZE * tileRow + yOffset), Color.Green, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
                RestartMessage();
            }
            if (_ball.CheckDefeat(this))
            {
                int xOffset = 32;
                Globals.SpriteBatch.DrawString(_font, "You Lose!", new Vector2(MiddleX - (MiddleX / 4) + xOffset, TEXTURE_SIZE * tileRow + yOffset), Color.Red, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
                RestartMessage();
            }
        }

        private void RestartMessage()
        {
            int xOffset = 18;
            int yOffset = 4;
            int tileRow = 8;
            Globals.SpriteBatch.DrawString(_font, "Press R to restart the game", new Vector2(MiddleX - LeftX + xOffset, TEXTURE_SIZE * tileRow + yOffset), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
        }

        public void ResetBallPaddlePosition()
        {
            _ball.Run = false;
            _paddle.Position = new(MiddleX - TEXTURE_SIZE / 2, (Globals.ScreenResolution.Y / 2) * 1.7f);
            var ballYPos = _paddle.Position.Y - _paddle.Texture.Height / 2 - 7;
            _ball.ResetBallDirection(new(MiddleX - TEXTURE_SIZE, ballYPos));
        }
    }
}

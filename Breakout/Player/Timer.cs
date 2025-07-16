using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout.Player
{
    public class Timer
    {
        private readonly SpriteFont _font;
        private float _time;
        private float _minutes;
        private float _seconds;
        public bool Run;

        public Timer(SpriteFont font)
        {
            _font = font;
            Run = false;
            _time = 0f;
            _minutes = 0f;
            _seconds = 0f;
        }

        public string GameTimer()
        {
            if (Run)
            {
                _time += Globals.Time;
                _seconds = (float)Math.Floor(_time % 60);
                _minutes = (float)Math.Floor(_time / 60);
                return string.Format("{0:00}:{1:00}", _minutes, _seconds);
            }
            return string.Format("{0:00}:{1:00}", _minutes, _seconds);
        }

        public void DrawTimer()
        {
            var textureSize = 32;
            var offset = 9;
            int tileRow = 9;
            Globals.SpriteBatch.DrawString(_font, "Timer:", new Vector2(textureSize + textureSize / 2, (textureSize * tileRow) - textureSize / 2), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.4f);
            Globals.SpriteBatch.DrawString(_font, GameTimer(), new Vector2(textureSize + textureSize / 2, (textureSize * (tileRow + 1)) - textureSize / 2 - offset), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.4f);
        }
    }
}

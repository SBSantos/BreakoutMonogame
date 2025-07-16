using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Breakout.Player
{
    public class Score
    {
        private readonly SpriteFont _font;
        private int _score;

        public Score(SpriteFont font)
        {
            _font = font;
            _score = 0;
        }

        public string GameScore()
        {
            return string.Format("{0}", _score);
        }

        public void IncreaseScore()
        {
            _score++;
        }

        public void DrawScore()
        {
            int textureSize = 32;
            int offset = 9;
            Globals.SpriteBatch.DrawString(_font, "Score:", new Vector2(textureSize + textureSize / 2, textureSize + textureSize / 2), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.4f);
            Globals.SpriteBatch.DrawString(_font, GameScore(), new Vector2(textureSize + textureSize / 2, textureSize * 2 + textureSize / 2 - offset), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.4f);
        }
    }
}

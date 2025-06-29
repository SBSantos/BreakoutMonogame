using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout.Manager
{
    public interface IHitable
    {
        Rectangle HitboxRectangle { get; }

        Vector2 Position { get; }
    }
}

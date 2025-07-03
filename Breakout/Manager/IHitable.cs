using Microsoft.Xna.Framework;

namespace Breakout.Manager
{
    interface IHitable
    {
        Rectangle HitboxRectangle { get; }
    }
}

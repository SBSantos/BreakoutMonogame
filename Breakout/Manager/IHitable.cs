using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout.Manager
{
    public interface IHitable
    {
        /// <summary>
        /// A hitbox rectangle for a sprite. Set your width and height values based on your sprite texture.
        /// </summary>
        Rectangle HitboxRectangle { get; }
    }
}

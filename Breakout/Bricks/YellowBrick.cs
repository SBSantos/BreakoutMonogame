using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout.Bricks
{
    class YellowBrick : Brick
    {
        public YellowBrick(Texture2D texture, Vector2 position) : base(texture, position)
        {
            Texture = Globals.Content.Load<Texture2D>("Textures/Bricks_Textures");
            SrcRectangle = new Rectangle(0, 0, 32, 32);
        }

        public override void Draw()
        {
            Globals.SpriteBatch.Draw(Texture, BrickRectangle, SrcRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f);

            // Brick hitbox
            //Globals.SpriteBatch.Draw(HitboxManager.HitboxTexture, HitboxManager.HitboxRectangle, SrcRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f);
        }
    }
}

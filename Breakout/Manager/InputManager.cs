using Microsoft.Xna.Framework.Input;

namespace Breakout.Manager
{
    public static class InputManager
    {
        private static KeyboardState _lastKB;
        public static bool SpacePressed { get; set; }
        public static bool RPressed { get; private set; }


        public static bool IsKeyDown(Keys key)
        {
            return Keyboard.GetState().IsKeyDown(key);
        }

        public static void Update()
        {
            var kb = Keyboard.GetState();
            SpacePressed = kb.IsKeyDown(Keys.Space) && _lastKB.IsKeyUp(Keys.Space);
            RPressed = kb.IsKeyDown(Keys.R) && _lastKB.IsKeyUp(Keys.R);
            _lastKB = kb;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public static class InputManager
    {
        private static KeyboardState _lastKB;
        public static bool SpacePressed { get; set; }//Evita o loop quando determinada tecla é pressionada apenas uma vez
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
            //IsKeyUp é a linha que evita o loop. (Obs: "X" em "Keys.X" refere-se a qualquer tecla, mude para a que desejar)
            _lastKB = kb;
        }
    }
}

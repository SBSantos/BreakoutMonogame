using System;
using System.Collections.Generic;
using Breakout.Manager;
using Breakout.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout.Bricks
{
    public class Map
    {
        private Texture2D _recTexture;
        private readonly Texture2D[] _textures =
        {
                Globals.Content.Load<Texture2D>("Textures/Red_Brick"),
                Globals.Content.Load<Texture2D>("Textures/Pink_Brick"),
                Globals.Content.Load<Texture2D>("Textures/Blue_Brick"),
                Globals.Content.Load<Texture2D>("Textures/Cyan_Brick"),
                Globals.Content.Load<Texture2D>("Textures/Green_Brick"),
                Globals.Content.Load<Texture2D>("Textures/Yellow_Brick"),
        };
        private Rectangle _newRect;
        private Point _screenSize;
        private readonly int _widthOffset = 8;
        private readonly int _heightOffset = 16;
        private Point _tileSize;
        private Brick[,] _bricks;
        public HashSet<Brick> ListBrick { get; set; }

        public Map()
        {
            _tileSize = new Point(32 + _widthOffset, _heightOffset);
            _screenSize = new Point(Globals.ScreenResolution.X / _tileSize.X, Globals.ScreenResolution.Y / _tileSize.Y);
            _bricks = new Brick[_screenSize.X, _screenSize.Y];
            ListBrick = new HashSet<Brick>();
            ListBricks();
        }

        public void Draw()
        {
            //_recTexture = new Texture2D(Globals.GraphicsDevice, 1, 1);
            //_recTexture.SetData([new Color(Color.Red, 0.1f)]);
            for (int i = 0; i < _screenSize.X; i++)
            {
                for (int j = 0; j < _screenSize.Y; j++)
                {
                    if (j < 6 && i > 3 && i < 12)
                    {
                        if (!_bricks[i, j].Active)
                        {
                            _bricks[i, j].Draw();
                        }
                    }
                }
            }
            //Globals.SpriteBatch.Draw(_recTexture, _newRect, Color.White);
        }
        
        public void CheckBrickCollision(Ball ball)
        {
            _newRect = ball.CalculateBound(ball.Position);
            foreach (var brick in ListBrick)
            {
                if (_newRect.Intersects(brick.HitboxRectangle))
                {
                    brick.Active = true;
                    ball.Score++;
                    ball.NumberBricksBroken++;
                    ball.Direction.Y = -ball.Direction.Y;
                    ListBrick.Remove(brick);
                }
            }
        }

        public bool CheckWin(GameManager gameManager)
        {
            if (ListBrick.Count == 0) { return gameManager.Victory = true; }
            return false;
        }

        public void ListBricks()
        {
            int index = 0;
            for (int i = 0; i < _screenSize.X; i++)
            {
                for (int j = 0; j < _screenSize.Y; j++)
                {
                    if (j < 6 && i > 3 && i < 12)
                    {
                        switch (j)
                        {
                            case 0: index = 0; break;
                            case 1: index = 1; break;
                            case 2: index = 2; break;
                            case 3: index = 3; break;
                            case 4: index = 4; break;
                            case 5: index = 5; break;
                        }

                        var xpos = i * _tileSize.X - 11;
                        var yPos = j * _tileSize.Y + 48;

                        _bricks[i, j] = new Brick(_textures[index], new Vector2(xpos, yPos));
                        ListBrick.Add(_bricks[i, j]);
                    }
                }
            }
        }

        public void ResetBrick()
        {
            _ = new Map();
            {
                ListBrick.Clear();
                ListBrick = new HashSet<Brick>();
                ListBricks();
            }
        }
    }
}

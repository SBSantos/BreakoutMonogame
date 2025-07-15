using System.Collections.Generic;
using System.IO;
using Breakout.Bricks;
using Breakout.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout.Manager
{
    public class Map
    {
        private Texture2D _recTexture;
        private readonly Texture2D[] _bricksTextures =
        {
                Globals.Content.Load<Texture2D>("Textures/Red_Brick"),
                Globals.Content.Load<Texture2D>("Textures/Pink_Brick"),
                Globals.Content.Load<Texture2D>("Textures/Blue_Brick"),
                Globals.Content.Load<Texture2D>("Textures/Cyan_Brick"),
                Globals.Content.Load<Texture2D>("Textures/Green_Brick"),
                Globals.Content.Load<Texture2D>("Textures/Yellow_Brick"),
        };
        private readonly Texture2D _spritesheet = Globals.Content.Load<Texture2D>("Textures/Breakout_spritesheet");
        private Rectangle _newRect;
        private Point _screenSize;
        private readonly int _widthOffset = 8;
        private readonly int _heightOffset = 4;
        private readonly int _brickSpriteGap = 4;
        private Point _brickSize;
        private Brick[,] _bricks;
        private Point _tileSize;
        private Dictionary<Vector2, int> _foreground;
        public HashSet<Brick> ListBrick { get; set; }

        public Map()
        {
            _tileSize = new Point(_bricksTextures[0].Width, _bricksTextures[0].Height);
            _brickSize = new Point(_bricksTextures[0].Width - _widthOffset - _brickSpriteGap, _bricksTextures[0].Height - _heightOffset);
            _screenSize = new Point(Globals.ScreenResolution.X / _tileSize.X, Globals.ScreenResolution.Y / _tileSize.Y);

            _bricks = new Brick[_screenSize.X, _screenSize.Y];
            ListBrick = new HashSet<Brick>();
            ListBricks();

            _foreground = LoadMap("../../../Data/Breakout_Foreground.csv");
        }

        public void Draw()
        {
            //_recTexture = new Texture2D(Globals.GraphicsDevice, 1, 1);
            //_recTexture.SetData([new Color(Color.Red, 0.1f)]);
            for (int i = 0; i < _screenSize.X; i++)
            {
                for (int j = 0; j < _screenSize.Y; j++)
                {
                    if (j < 6 && i > 3 && i < 24)
                    {
                        if (!_bricks[i, j].Active)
                        {
                            _bricks[i, j].Draw();
                        }
                    }
                }
            }
            DrawMap();
            //Globals.SpriteBatch.Draw(_recTexture, _newRect, Color.White);
        }

        public void CheckBrickCollision(Ball ball, Score score)
        {
            _newRect = ball.CalculateBound(ball.Position);
            foreach (var brick in ListBrick)
            {
                if (_newRect.Intersects(brick.HitboxRectangle))
                {
                    brick.Active = true;
                    score.IncreaseScore();
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
                    if (j < 6 && i > 3 && i < 24)
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

                        var xpos = i * _brickSize.X + _tileSize.X * 2 + _brickSize.X / 2;
                        var yPos = j * _brickSize.Y + _tileSize.Y * 2;

                        _bricks[i, j] = new Brick(_bricksTextures[index], new Vector2(xpos, yPos));
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

        private Dictionary<Vector2, int> LoadMap(string path)
        {
            Dictionary<Vector2, int> result = new();

            StreamReader reader = new(path);

            int y = 0;
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] item = line.Split(',');
                for (int x = 0; x < item.Length; x++)
                {
                    if (int.TryParse(item[x], out int value))
                    {
                        if (value > -1) { result[new Vector2(x, y)] = value; }
                    }
                }
                y++;
            }
            return result;
        }

        public void DrawMap()
        {
            int tileSize = 32;
            int tilesPerRow = 5;
            foreach (var item in _foreground)
            {
                Rectangle destination = new((int)item.Key.X * tileSize, (int)item.Key.Y * tileSize, tileSize, tileSize);

                int x = item.Value % tilesPerRow;
                int y = item.Value / tilesPerRow;

                Rectangle source = new(x * tileSize, y * tileSize, tileSize, tileSize);

                Globals.SpriteBatch.Draw(_spritesheet, destination, source, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.2f);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using Breakout.Bricks;
using Breakout.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout.Manager
{
    public class Map
    {
        private Texture2D _recTexture; // To view Ball hitbox
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
        private readonly int _widthOffset = 8;
        private readonly int _heightOffset = 4;
        private readonly int _brickSpriteGap = 4;

        private Rectangle _newRect;
        private Point _screenSize;
        private Point _brickSize;
        private Point _tileSize;
        private Brick[,] _bricks;
        private Tile[,] _tiles;
        private Dictionary<Vector2, int> _foreground;
        public HashSet<Brick> ListBrick { get; set; }

        public Map()
        {
            _tileSize = new Point(_bricksTextures[0].Width, _bricksTextures[0].Height);
            _brickSize = new Point(_bricksTextures[0].Width - _widthOffset - _brickSpriteGap, _bricksTextures[0].Height - _heightOffset);
            _screenSize = new Point(Globals.ScreenResolution.X / _tileSize.X, Globals.ScreenResolution.Y / _tileSize.Y);

            _tiles = new Tile[_tileSize.X, _tileSize.Y];
            CreateTiles();

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
                    _tiles[i, j].Draw();
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
                    brick.BrickSound.Play();
                    score.IncreaseScore();
                    ball.Direction.Y = -ball.Direction.Y;
                    ListBrick.Remove(brick);
                }
            }
        }

        public bool CheckWin(GameManager gameManager)
        {
            if (ListBrick.Count == 0) 
            {
                return gameManager.Victory = true; 
            }
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
            Random r = new();
            int tilesPerRow = 6;
            foreach (var item in _foreground)
            {
                Rectangle destination = new((int)item.Key.X * _tileSize.X, (int)item.Key.Y * _tileSize.Y, _tileSize.X, _tileSize.Y);

                int x = item.Value % tilesPerRow;
                int y = item.Value / tilesPerRow;

                Rectangle source = new(x * _tileSize.X, y * _tileSize.Y, _tileSize.X, _tileSize.Y);

                Globals.SpriteBatch.Draw(_spritesheet, destination, source, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.2f);
            }
        }
        
        public void CreateTiles()
        {
            Random r = new();
            int tilesPerRow = 6;
            for (int i = 0; i < _screenSize.X; i++)
            {
                for (int j = 0; j < _screenSize.Y; j++)
                {
                    var xPos = i * _tileSize.X;
                    var yPos = j * _tileSize.Y;

                    Rectangle destination = new(xPos, yPos, _tileSize.X, _tileSize.Y);

                    int x;
                    int y = r.Next(tilesPerRow) < 4 ? 4 : 5;

                    switch (y) 
                    {
                        case 4: x = 5; break;
                        default: x = r.Next(tilesPerRow); break;
                    }

                    Rectangle source = new(x * _tileSize.X, y * _tileSize.Y, _tileSize.X, _tileSize.Y);

                    _tiles[i, j] = new(_spritesheet, destination, source, new Vector2(xPos, yPos));
                }
            }
        }
    }
}

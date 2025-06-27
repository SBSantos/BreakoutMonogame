using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Breakout.Player;
using Breakout.Manager;

namespace Breakout;

public class GameLoop : Game
{
    /*
    private Paddle _paddle;
    private Vector2[] _position = new Vector2[3];
    private List<Bricks> _bricks;
    private Ball _ball;
    private SpriteFont[] _font = new SpriteFont[2];
    private SoundEffect[] _sound = new SoundEffect[2];

    private int _bricksHeight = 8;
    private int _bricksWidth = 60;
    private int _rows = 8;
    private int _column = 14;
    private int _numBricksBroke = 0;
    private int _score { get; set; }
    private float _time;

    private string _scoreText;
    private string _timerText;

    private const int _BRICKS_DIV = 2;
    private const int SPACE = 100;

    private bool _victory { get; set; }
    private bool _defeat { get; set; }
    */
    private GameManager _gameManager;

    public GameLoop()
    {
        Globals.Graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        Globals.Content = Content;
        _gameManager = new();
        Globals.Resolution();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        Globals.SpriteBatch = new SpriteBatch(GraphicsDevice);
        Globals.GraphicsDevice = GraphicsDevice;
        /*
        _position[0] = new(Globals.WIDTH / 20, 32); //EM CIMA
        _position[1] = new((Globals.WIDTH / 5) - 32, (Globals.HEIGHT / 2) - 32); //MEIO
        _position[2] = new((Globals.WIDTH / 5) - 72, (Globals.HEIGHT / 2) - 256);

        // TODO: use this.Content to load your game content here
        _paddle = new(new((Globals.WIDTH / 2) - 30, Globals.HEIGHT / 2), Keys.A, Keys.D);
        _bricks = new List<Bricks>();
        _ball = new(new(Globals.WIDTH / 2, (Globals.HEIGHT / 2) + 340));

        ListBricks();
        
        _font[0] = Content.Load<SpriteFont>("fontmenor"); //EM CIMA
        _font[1] = Content.Load<SpriteFont>("font"); // MEIO

        _sound[0] = Content.Load<SoundEffect>("Defeat");
        _sound[1] = Content.Load<SoundEffect>("Victory");*/
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _gameManager.Update(gameTime);
        /*
        _paddle.Update();
        _paddle.CheckPaddleBallCollision(_ball);

        _ball.Update();

        if (InputManager.SpacePressed && !_ball.Run)
        {
            _ball.Run = true;
        }

        foreach (var item in _bricks)
        {
            if (item.CheckBallCollision(_ball))
            {
                item.Active = false;
                _numBricksBroke++;
                _score += 10;
            }
        }

        if (_victory || _defeat)
        {
            _paddle.Speed = 0f;
            _ball.Speed = 0f;

            if (InputManager.RPressed)
            {
                foreach (var item in _bricks)
                {
                    Components.Remove(item);
                }

                InputManager.SpacePressed = false;
                Reset();
            }
            return;
        }

        CheckDefeat();
        CheckWin();
        Score();
        Timer();*/

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(08, 00, 35));
        /*Globals.SpriteBatch.Begin();
        _paddle.Draw();
        _ball.Draw();

        Globals.SpriteBatch.DrawString(_font[0], Score(), _position[0], new Color(62, 16, 196) * 0.4f);
        Globals.SpriteBatch.DrawString(_font[1], Timer(), _position[1], new Color(62, 16, 196) * 0.4f);

        if (_victory)
        {
            Globals.SpriteBatch.DrawString(_font[1], " Voce\nVenceu", _position[2], Color.Gold * 1f);
        }

        if (_defeat)
        {
            Globals.SpriteBatch.DrawString(_font[1], " Voce\nPerdeu", _position[2], Color.Red * 1f);
        }

        Globals.SpriteBatch.End();*/
        _gameManager.Draw();

        base.Draw(gameTime);
    }
    /*
    public void ListBricks()
    {
        for (int i = 0; i < Globals.WIDTH / _bricksWidth; i++)
        {
            for (int j = 0; j < _rows / 4; j++)
            {
                _bricks.Add(new Bricks(this, new(0, 0), new(i * _bricksWidth + i, (j * _bricksHeight + j) + SPACE, _bricksWidth, _bricksHeight), new(Color.Red, 1)));
            }
        }

        for (int i = 0; i < Globals.WIDTH / _bricksWidth; i++)
        {
            for (int j = 0; j < _rows / 4; j++)
            {
                _bricks.Add(new Bricks(this, new(0, 0), new(i * _bricksWidth + i, (j * _bricksHeight + j) + SPACE + ((_bricksHeight * 4) / 2) + _BRICKS_DIV, _bricksWidth, _bricksHeight), new(Color.Orange, 1)));
            }
        }

        for (int i = 0; i < Globals.WIDTH / _bricksWidth; i++)
        {
            for (int j = 0; j < _rows / 4; j++)
            {
                _bricks.Add(new Bricks(this, new(0, 0), new(i * _bricksWidth + i, (j * _bricksHeight + j) + SPACE + ((_bricksHeight * 8) / 2) + _BRICKS_DIV * 2, _bricksWidth, _bricksHeight), new(Color.Green, 1)));
            }
        }

        for (int i = 0; i < Globals.WIDTH / _bricksWidth; i++)
        {
            for (int j = 0; j < _rows / 4; j++)
            {
                _bricks.Add(new Bricks(this, new(0, 0), new(i * _bricksWidth + i, (j * _bricksHeight + j) + SPACE + ((_bricksHeight * 12) / 2) + _BRICKS_DIV * 3, _bricksWidth, _bricksHeight), new(Color.Yellow, 1)));
            }
        }

        foreach (var b in _bricks)
        {
            Components.Add(b);
        }
    }

    public void CheckDefeat()
    {
        const float h = Globals.HEIGHT;

        switch(_ball.Position.Y)
        {
            case >= h:
            _defeat = true;
            _sound[0].Play();
                break;
        }
    }

    public void CheckWin()
    {
        switch(_numBricksBroke)
        {
            case >= 80:
            _victory = true;
            _sound[1].Play();
                break;
        }
    }

    public string Score()
    {
        return _scoreText = string.Format("Score:{0}", _score);
    }

    public string Timer()
    {
        _time += Globals.Time;
        int min = (int)MathF.Floor(_time / 60);
        int sec = (int)MathF.Floor(_time % 60);
        return _timerText = string.Format("{0:00}:{1:00}", min, sec);
    }*/
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Breakout.Manager;
using System;

namespace Breakout;

public class GameLoop : Game
{
    private GameManager _gameManager;
    private bool _isResing;
    private Matrix _scale;
    private int _virtualWidth = Globals.ScreenResolution.X;
    private int _virtualHeight = Globals.ScreenResolution.Y;
    private Viewport _viewport;

    public GameLoop()
    {
        Globals.Graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Window.AllowUserResizing = true;
        Window.ClientSizeChanged += OnClientSizeChanged;
    }

    protected override void Initialize()
    {
        Globals.Content = Content;
        _gameManager = new();
        Globals.Resolution();
        UpdateScreenScaleMatrix();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        Globals.SpriteBatch = new SpriteBatch(GraphicsDevice);
        Globals.GraphicsDevice = GraphicsDevice;
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _gameManager.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(08, 00, 35));
        Globals.GraphicsDevice.Viewport = _viewport;

        Globals.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _scale);
        _gameManager.Draw();
        Globals.SpriteBatch.End();

        base.Draw(gameTime);
    }

    public void OnClientSizeChanged(object sender, EventArgs e)
    {
        if (!_isResing && Window.ClientBounds.Width > 0 && Window.ClientBounds.Height > 0)
        {
            _isResing = true;
            UpdateScreenScaleMatrix();
            _isResing = false;
        }
    }

    public void UpdateScreenScaleMatrix()
    {
        float screenWidth = GraphicsDevice.PresentationParameters.BackBufferWidth;
        float screenHeight = GraphicsDevice.PresentationParameters.BackBufferHeight;

        if (screenWidth / Globals.ScreenResolution.X > screenHeight / Globals.ScreenResolution.Y)
        {
            float aspect = screenHeight / Globals.ScreenResolution.Y;
            _virtualWidth = (int)aspect * Globals.ScreenResolution.X;
            _virtualHeight = (int)screenHeight;
        }
        else
        {
            float aspect = screenWidth / Globals.ScreenResolution.X;
            _virtualWidth = (int)screenWidth;
            _virtualHeight = (int)aspect * Globals.ScreenResolution.Y;
        }

        _scale = Matrix.CreateScale(_virtualWidth / Globals.ScreenResolution.X);

        _viewport = new()
        {
            X = (int)(screenWidth / 2 - _virtualWidth / 2),
            Y = (int)(screenHeight / 2 - _virtualHeight / 2),
            Width = _virtualWidth,
            Height = _virtualHeight,
            MinDepth = 0,
            MaxDepth = 1
        };
    }
}

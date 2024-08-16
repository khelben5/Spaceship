using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Spaceship;

public class Game1 : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _shipSprite;
    private Texture2D _asteroidSprite;
    private Texture2D _spaceSprite;

    private SpriteFont _gameFont;
    private SpriteFont _timerFont;

    private Controller _controller;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        SetUpScreenSize();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        LoadSprites();
        LoadFonts();
        InitialiseController();
    }

    protected override void Update(GameTime gameTime)
    {
        ExitGameIfRequired();
        _controller.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        DrawBackground();
        DrawShip();
        DrawAsteroids();
        DrawMenu();
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void SetUpScreenSize()
    {
        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.PreferredBackBufferHeight = 720;
        _graphics.ApplyChanges();
    }

    private void LoadSprites()
    {
        _shipSprite = LoadSprite("ship");
        _asteroidSprite = LoadSprite("asteroid");
        _spaceSprite = LoadSprite("space");
    }

    private void LoadFonts()
    {
        _gameFont = LoadFont("spaceFont");
        _timerFont = LoadFont("timerFont");
    }

    private Texture2D LoadSprite(String name) => Content.Load<Texture2D>(name);

    private SpriteFont LoadFont(String name) => Content.Load<SpriteFont>(name);

    private void InitialiseController()
    {
        _controller = new(
            canvasSize: GetCanvasSize(),
            shipRadius: GetShipRadius(),
            asteroidRadius: GetAsteroidRadius()
        );
    }

    private Vector2 GetCanvasSize() => new(
        _graphics.PreferredBackBufferWidth,
        _graphics.PreferredBackBufferHeight
    );

    private float GetShipRadius() => _shipSprite.Width / 2;

    private float GetAsteroidRadius() => _asteroidSprite.Width / 2;

    private void ExitGameIfRequired()
    {
        if (ShouldExitGame()) Exit();
    }

    private bool ShouldExitGame() => IsBackButtonPressed() || IsEscapeKeyPressed();
    private bool IsBackButtonPressed() => GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed;
    private bool IsEscapeKeyPressed() => Keyboard.GetState().IsKeyDown(Keys.Escape);

    private void DrawBackground()
    {
        _spriteBatch.Draw(_spaceSprite, new Vector2(0, 0), Color.White);
    }

    private void DrawShip()
    {
        _spriteBatch.Draw(
            _shipSprite,
            ToDrawPosition(_controller.GetShipPosition(), _shipSprite),
            Color.White
        );
    }

    private void DrawAsteroids()
    {
        foreach (var asteroid in _controller.GetAsteroids())
        {
            _spriteBatch.Draw(
                _asteroidSprite,
                ToDrawPosition(asteroid.GetPosition(), _asteroidSprite),
                Color.White
            );
        }
    }

    private Vector2 ToDrawPosition(Vector2 position, Texture2D sprite) => ToDrawPosition(
        position,
        new Vector2(sprite.Width, sprite.Height)
    );

    private Vector2 ToDrawPosition(Vector2 position, Vector2 itemSize) => new Vector2(
        position.X - itemSize.X / 2,
        position.Y - itemSize.Y / 2
    );

    private void DrawMenu()
    {
        if (_controller.ShouldShowMenu())
        {
            string message = "Press ENTER to play";
            _spriteBatch.DrawString(
                _gameFont,
                message,
                computeMenuPosition(message),
                Color.White
            );
        }
    }

    private Vector2 computeMenuPosition(String message)
    {
        Vector2 textSize = _gameFont.MeasureString(message);
        return ToDrawPosition(new(GetCanvasSize().X / 2, 200), textSize);
    }
}

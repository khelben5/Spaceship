using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Spaceship;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _shipSprite;
    private Texture2D _asteroidSprite;
    private Texture2D _spaceSprite;

    private SpriteFont _spaceFont;
    private SpriteFont _timerFont;

    private Ship _ship = new Ship();
    private Asteroid _asteroid;

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
        _asteroid = new Asteroid(_asteroidSprite.Width / 2);
    }

    protected override void Update(GameTime gameTime)
    {
        ExitGameIfRequired();
        _ship.Update(gameTime);
        _asteroid.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        _spriteBatch.Draw(_spaceSprite, new Vector2(0, 0), Color.White);
        _spriteBatch.Draw(_shipSprite, ToRenderPosition(_ship.Position, _shipSprite), Color.White);
        _spriteBatch.Draw(_asteroidSprite, ToRenderPosition(_asteroid.Position, _asteroidSprite), Color.White);
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
        _spaceFont = LoadFont("spaceFont");
        _timerFont = LoadFont("timerFont");
    }

    private Texture2D LoadSprite(String name) => Content.Load<Texture2D>(name);

    private SpriteFont LoadFont(String name) => Content.Load<SpriteFont>(name);

    private void ExitGameIfRequired()
    {
        if (ShouldExitGame()) Exit();
    }

    private bool ShouldExitGame() => IsBackButtonPressed() || IsEscapeKeyPressed();
    private bool IsBackButtonPressed() => GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed;
    private bool IsEscapeKeyPressed() => Keyboard.GetState().IsKeyDown(Keys.Escape);

    private Vector2 ToRenderPosition(Vector2 position, Texture2D sprite) => new Vector2(
        position.X - sprite.Width / 2,
        position.Y - sprite.Height / 2
    );
}

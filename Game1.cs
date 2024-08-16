using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Spaceship;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D shipSprite;
    private Texture2D asteroidSprite;
    private Texture2D spaceSprite;

    private SpriteFont spaceFont;
    private SpriteFont timerFont;

    private Ship ship = new Ship();

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
    }

    protected override void Update(GameTime gameTime)
    {
        ExitGameIfRequired();
        ship.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        _spriteBatch.Draw(spaceSprite, new Vector2(0, 0), Color.White);
        _spriteBatch.Draw(shipSprite, ToRenderPosition(ship.position, shipSprite), Color.White);
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
        shipSprite = LoadSprite("ship");
        asteroidSprite = LoadSprite("asteroid");
        spaceSprite = LoadSprite("space");
    }

    private void LoadFonts()
    {
        spaceFont = LoadFont("spaceFont");
        timerFont = LoadFont("timerFont");
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

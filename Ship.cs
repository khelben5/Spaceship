using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Spaceship
{

    class Ship
    {
        private readonly Vector2 _defaultPosition;
        private Vector2 _position;

        private const float _speed = 200;

        public Ship(Vector2 canvasSize)
        {
            _defaultPosition = new(canvasSize.X / 2, canvasSize.Y / 2);
            _position = _defaultPosition;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (keyboardState.IsKeyDown(Keys.Down)) _position.Y += _speed * deltaSeconds;
            if (keyboardState.IsKeyDown(Keys.Up)) _position.Y -= _speed * deltaSeconds;
            if (keyboardState.IsKeyDown(Keys.Right)) _position.X += _speed * deltaSeconds;
            if (keyboardState.IsKeyDown(Keys.Left)) _position.X -= _speed * deltaSeconds;
        }

        public Vector2 GetPosition() => _position;
    }
}

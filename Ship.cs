using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Spaceship
{

    class Ship
    {
        private readonly Vector2 _defaultPosition;
        private readonly float _radius;
        private Vector2 _position;
        private Vector2 _canvasSize;

        private const float _speed = 200;

        public Ship(Vector2 canvasSize, float radius)
        {
            _canvasSize = canvasSize;
            _defaultPosition = new(canvasSize.X / 2, canvasSize.Y / 2);
            _position = _defaultPosition;
            _radius = radius;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float increment = GetIncrement(deltaTime);

            if (keyboardState.IsKeyDown(Keys.Down)) moveDown(increment);
            if (keyboardState.IsKeyDown(Keys.Up)) moveUp(increment);
            if (keyboardState.IsKeyDown(Keys.Right)) moveRight(increment);
            if (keyboardState.IsKeyDown(Keys.Left)) moveLeft(increment);
        }

        public Vector2 GetPosition() => _position;

        public float GetRadius() => _radius;

        public void Reset()
        {
            _position = _defaultPosition;
        }

        private float GetIncrement(float deltaTime) => _speed * deltaTime;

        private void moveDown(float increment)
        {
            float maxY = _canvasSize.Y - _radius;
            if (_position.Y + increment > maxY)
            {
                _position.Y = maxY;
                return;
            }
            _position.Y += increment;
        }

        private void moveUp(float increment)
        {
            float minY = _radius;
            if (_position.Y - increment < minY)
            {
                _position.Y = minY;
                return;
            }
            _position.Y -= increment;
        }

        private void moveRight(float increment)
        {
            float maxX = _canvasSize.X - _radius;
            if (_position.X + increment > maxX)
            {
                _position.X = maxX;
                return;
            }
            _position.X += increment;
        }

        private void moveLeft(float increment)
        {
            float minX = _radius;
            if (_position.X - increment < minX)
            {
                _position.X = minX;
                return;
            }
            _position.X -= increment;
        }
    }
}

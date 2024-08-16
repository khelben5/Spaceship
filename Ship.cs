using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Spaceship
{

    class Ship
    {
        public Vector2 Position = new Vector2(100, 100);

        private const float _speed = 200;

        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (keyboardState.IsKeyDown(Keys.Down)) Position.Y += _speed * deltaSeconds;
            if (keyboardState.IsKeyDown(Keys.Up)) Position.Y -= _speed * deltaSeconds;
            if (keyboardState.IsKeyDown(Keys.Right)) Position.X += _speed * deltaSeconds;
            if (keyboardState.IsKeyDown(Keys.Left)) Position.X -= _speed * deltaSeconds;
        }
    }
}

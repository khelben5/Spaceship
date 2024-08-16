using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Spaceship
{

    class Ship
    {
        public Vector2 position = new Vector2(100, 100);

        private const float speed = 200;

        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (keyboardState.IsKeyDown(Keys.Down)) position.Y += speed * deltaSeconds;
            if (keyboardState.IsKeyDown(Keys.Up)) position.Y -= speed * deltaSeconds;
            if (keyboardState.IsKeyDown(Keys.Right)) position.X += speed * deltaSeconds;
            if (keyboardState.IsKeyDown(Keys.Left)) position.X -= speed * deltaSeconds;
        }
    }
}

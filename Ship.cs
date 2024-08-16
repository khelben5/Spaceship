using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Spaceship
{

    class Ship
    {
        public Vector2 position = new Vector2(100, 100);

        private const float speed = 5f;

        public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Down)) position.Y += speed;
            if (keyboardState.IsKeyDown(Keys.Up)) position.Y -= speed;
            if (keyboardState.IsKeyDown(Keys.Right)) position.X += speed;
            if (keyboardState.IsKeyDown(Keys.Left)) position.X -= speed;
        }
    }
}

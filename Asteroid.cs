using Microsoft.Xna.Framework;

namespace Spaceship
{

    class Asteroid
    {
        public Vector2 Position = new Vector2(600, 300);

        private readonly float _radius;
        private readonly float _speed;

        public Asteroid(float radius, float speed = 240)
        {
            _radius = radius;
            _speed = speed;
        }

        public void Update(GameTime gameTime)
        {
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position.X -= _speed * deltaSeconds;
        }
    }
}

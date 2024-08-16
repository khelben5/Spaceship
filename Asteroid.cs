using Microsoft.Xna.Framework;

namespace Spaceship
{

    class Asteroid
    {
        private Vector2 _position;
        private readonly float _radius;
        private readonly float _speed;

        public Asteroid(Vector2 position, float radius, float speed)
        {
            _position = position;
            _radius = radius;
            _speed = speed;
        }

        public void Update(GameTime gameTime)
        {
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _position.X -= _speed * deltaSeconds;
        }

        public Vector2 GetPosition() => _position;

        public float GetRadius() => _radius;
    }
}

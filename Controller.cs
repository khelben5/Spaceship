using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Spaceship
{

    class Controller
    {
        private readonly List<Asteroid> _asteroids = new List<Asteroid>();
        private readonly Ship _ship;
        private Vector2 _canvasSize;
        private readonly float _asteroidRadius;
        private float _asteroidSpeed = 200;
        private float _maxTimer = 2;
        private float _currentTimer;
        private bool _inGame = false;

        public Controller(
            Vector2 canvasSize,
            float asteroidRadius
        )
        {
            _ship = new(canvasSize);
            _canvasSize = canvasSize;
            _asteroidRadius = asteroidRadius;
            _currentTimer = _maxTimer;
        }

        public void Update(GameTime gameTime)
        {
            if (!_inGame)
            {
                DetectGameStart();
                return;
            }

            UpdateShip(gameTime);
            UpdateAsteroids(gameTime);
            GenerateNewAsteroidIfNeeded(gameTime);
        }

        public List<Asteroid> GetAsteroids() => _asteroids;

        public Vector2 GetShipPosition() => _ship.GetPosition();

        public bool ShouldShowMenu() => !_inGame;

        private void DetectGameStart()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                _inGame = true;
            }
        }

        private void UpdateShip(GameTime gameTime)
        {
            _ship.Update(gameTime);
        }

        private void UpdateAsteroids(GameTime gameTime)
        {
            foreach (var asteroid in _asteroids)
            {
                asteroid.Update(gameTime);
            }
        }

        private void GenerateNewAsteroidIfNeeded(GameTime gameTime)
        {
            if (!ShouldGenerateANewAsteroid(gameTime)) return;

            UpdateTimer();
            GenerateNewAsteroid();
            IncreaseDifficulty();
        }

        private bool ShouldGenerateANewAsteroid(GameTime gameTime)
        {
            _currentTimer -= GetDeltaSeconds(gameTime);
            return _currentTimer <= 0;
        }

        private float GetDeltaSeconds(GameTime gameTime) => (float)gameTime.ElapsedGameTime.TotalSeconds;

        private void UpdateTimer()
        {
            _currentTimer = _maxTimer;
        }

        private void GenerateNewAsteroid()
        {
            _asteroids.Add(GenerateAsteroidAt(RandomAsteroidPosition()));
        }

        private Vector2 RandomAsteroidPosition() => new Vector2(
            _canvasSize.X + _asteroidRadius,
            Random.Shared.Next((int)_asteroidRadius, (int)(_canvasSize.Y - _asteroidRadius))
        );

        private Asteroid GenerateAsteroidAt(Vector2 position) => new(position, _asteroidRadius, _asteroidSpeed);

        private void IncreaseDifficulty()
        {
            IncreaseAsteroidFrequency();
            IncreaseAsteroidSpeed();
        }

        private void IncreaseAsteroidFrequency()
        {
            if (_maxTimer > 0.5) _maxTimer -= 0.1f;
        }

        private void IncreaseAsteroidSpeed()
        {
            if (_asteroidSpeed < 720) _asteroidSpeed += 4;
        }
    }
}

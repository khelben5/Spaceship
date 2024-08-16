using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Spaceship
{

    class Controller
    {
        private const float _defaultAsteroidSpeed = 200;
        private const float _defaultTimer = 2;

        private readonly List<Asteroid> _asteroids = new List<Asteroid>();
        private readonly Ship _ship;
        private Vector2 _canvasSize;
        private readonly float _asteroidRadius;
        private float _asteroidSpeed = _defaultAsteroidSpeed;
        private float _maxTimer = _defaultTimer;
        private float _currentTimer;
        private bool _inGame = false;
        private double _score = 0;

        public Controller(
            Vector2 canvasSize,
            float shipRadius,
            float asteroidRadius
        )
        {
            _ship = new(canvasSize, shipRadius);
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
            UpdateScore(gameTime);
        }

        public List<Asteroid> GetAsteroids() => _asteroids;

        public Vector2 GetShipPosition() => _ship.GetPosition();

        public double GetScore() => _score;

        public bool ShouldShowMenu() => !_inGame;

        private void DetectGameStart()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                _score = 0;
                _inGame = true;
            }
        }

        private void UpdateShip(GameTime gameTime)
        {
            _ship.Update(gameTime);
        }

        private void UpdateAsteroids(GameTime gameTime)
        {
            for (int i = 0; _inGame && i < _asteroids.Count; i++)
            {
                Asteroid asteroid = _asteroids[i];
                asteroid.Update(gameTime);
                DetectCollision(asteroid);
            }
        }

        private void DetectCollision(Asteroid asteroid)
        {
            float distance = Vector2.Distance(_ship.GetPosition(), asteroid.GetPosition());
            bool collision = distance < asteroid.GetRadius() + _ship.GetRadius();
            if (collision)
            {
                ResetGame();
                _inGame = false;
            }
        }

        private void ResetGame()
        {
            _ship.Reset();
            _asteroids.Clear();
            _asteroidSpeed = _defaultAsteroidSpeed;
            _maxTimer = _defaultTimer;
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
            _currentTimer -= GetDelta(gameTime);
            return _currentTimer <= 0;
        }

        private float GetDelta(GameTime gameTime) => (float)gameTime.ElapsedGameTime.TotalSeconds;

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

        private void UpdateScore(GameTime gameTime)
        {
            _score += gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}

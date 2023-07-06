using UnityEngine;
using Zenject;
using System;

namespace DesignPatterns.Flyweight
{
    public class FlyweightGameManager : MonoBehaviour
    {
        private float _remainingTime;
        private int _enemyCount;
        private int _playerScore;
        private FlyweightUIManager _flyweightUIManager;
        private EnemySpawner _enemySpawner;
        public event Action ScoreEvent;

        public float RemainingTime
        {
            get => _remainingTime;

            set
            {
                if (_remainingTime < 0) ;

                _remainingTime = value;
                _flyweightUIManager.UpdateTimeText();
            }
        }

        public int PlayerScore
        {
            get => _playerScore;

            set
            {
                if (_playerScore < 0) return;
                _playerScore = value;
            }
        }

        public int EnemyCount
        {
            get => _enemyCount;

            set
            {
                if (_enemyCount < 0) return;
                _enemyCount = value;
            }
        }

        [Inject]
        private void OnInstaller(FlyweightUIManager flyweightUIManager, EnemySpawner enemySpawner)
        {
            _flyweightUIManager = flyweightUIManager;
            _enemySpawner = enemySpawner;
        }

        private void OnEnable()
        {
            ScoreEvent += UpdateScore;
        }

        private void OnDisable()
        {
            ScoreEvent -= UpdateScore;
        }

        private void Update()
        {
            Timer();
        }

        private void Timer()
        {
            if (RemainingTime <= 0) return;

            RemainingTime -= Time.deltaTime;
            _flyweightUIManager.UpdateTimeText();
            if (RemainingTime <= 0)
            {
                CheckGameState();
            }
        }


        public void UpdateScore()
        {
            PlayerScore++;
            EnemyCount--;
        }

        public void ScoreEventInitializer()
        {
            ScoreEvent?.Invoke();
        }

        private void CheckGameState()
        {
            _flyweightUIManager.GameStatePanel(_enemyCount <= 0);
        }

        public void RestartGame()
        {
            PlayerScore = 0;
            _enemySpawner.DisableAllEnemies();
            _enemySpawner.Initializer();
            _flyweightUIManager.CloseGameStatePanels();
            _flyweightUIManager.UpdateScoreText();
            _flyweightUIManager.UpdateTimeText();
        }
    }
}
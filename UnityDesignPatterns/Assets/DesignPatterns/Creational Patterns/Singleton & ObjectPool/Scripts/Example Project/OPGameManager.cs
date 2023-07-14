using System;
using UnityEngine;

namespace DesignPatterns.SingletonObjectPool
{
    public class OPGameManager : MonoBehaviour
    {
        private EnemySpawner _enemySpawner;
        private OPUIManager _opUIManager;
        public GameObject Player;
        public int score = 0;
        public Action _restartGame;

        private void OnEnable()
        {
            _restartGame += RestartGame;
        }

        private void OnDisable()
        {
            _restartGame -= RestartGame;
        }

        private void Start()
        {
            _enemySpawner = SingletonContainer.Instance.EnemySpawner;
            _opUIManager = SingletonContainer.Instance.OpUIManager;
        }

        public void AddScore()
        {
            score++;
            _opUIManager.UpdateScore(score);
            CheckWinCondition();
        }

        private void CheckWinCondition()
        {
            if (score >= _enemySpawner.totalSpawnCount)
            {
                Debug.Log("You Win!");
                _opUIManager.ShowWinPanel();
            }
        }

        public void GameOver()
        {
            Debug.Log("Game Over");
            _opUIManager.ShowLosePanel();
            _enemySpawner.DeactivateAllEnemies();
        }

        public void RestartGame()
        {
            score = 0;
        }
    }
}
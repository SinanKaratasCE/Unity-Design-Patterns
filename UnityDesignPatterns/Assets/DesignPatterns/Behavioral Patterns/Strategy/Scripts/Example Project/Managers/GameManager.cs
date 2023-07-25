using System;
using UnityEngine;
using Zenject;

namespace DesignPatterns.Strategy
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Castle castle;
        private UIManager _uiManager;
        private EnemySpawner _enemySpawner;

        public int totalKilledEnemy;

        private void Start()
        {
            LockCursor();
        }

        [Inject]
        private void OnInstaller(UIManager uiManager, EnemySpawner enemySpawner)
        {
            _uiManager = uiManager;
            _enemySpawner = enemySpawner;
        }

        public void GameOver()
        {
            _uiManager.OnGameOver();
            _enemySpawner.DeactivateAllEnemies();
            ReleaseCursor();
        }

        public void EnemyKilled()
        {
            totalKilledEnemy++;
            if (totalKilledEnemy >= _enemySpawner.totalSpawnCount)
            {
                _uiManager.OnGameWin();
                ReleaseCursor();
            }
        }

        public void RestartGame()
        {
            _enemySpawner.Reset();
            _uiManager.ResetPanels();
            ResetGameValues();
            castle.ResetHealth();
        }

        private void ResetGameValues()
        {
            totalKilledEnemy = 0;
            LockCursor();
        }

        private void ReleaseCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
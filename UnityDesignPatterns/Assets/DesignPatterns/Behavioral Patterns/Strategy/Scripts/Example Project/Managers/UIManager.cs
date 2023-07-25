using System;
using UnityEngine;
using Zenject;
using UnityEngine.UI;

namespace DesignPatterns.Strategy
{
    public class UIManager : MonoBehaviour
    {
        private GameManager _gameManager;
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private GameObject gameWinPanel;
        [SerializeField] private Button restartButton;

        private void Start()
        {
            restartButton.onClick.AddListener(_gameManager.RestartGame);
        }

        [Inject]
        private void OnInstaller(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public void OnGameOver()
        {
            gameOverPanel.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }

        public void OnGameWin()
        {
            gameWinPanel.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }

        private void DeactivateAllPanels()
        {
            gameOverPanel.SetActive(false);
            gameWinPanel.SetActive(false);
            restartButton.gameObject.SetActive(false);
        }

        public void ResetPanels()
        {
            DeactivateAllPanels();
        }
    }
}
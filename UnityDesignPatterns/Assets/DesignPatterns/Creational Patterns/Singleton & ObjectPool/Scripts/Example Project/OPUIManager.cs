using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DesignPatterns.SingletonObjectPool
{
    public class OPUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject _losePanel, _winPanel;
        [SerializeField] private Button restartButton;
        [SerializeField] private TMP_Text scoreText;

        private EnemySpawner _enemySpawner;

        private void OnEnable()
        {
            SingletonContainer.Instance.OpGameManager._restartGame += ResetUI;
        }

        private void OnDisable()
        {
            SingletonContainer.Instance.OpGameManager._restartGame -= ResetUI;
        }

        private void Start()
        {
            _enemySpawner = SingletonContainer.Instance.EnemySpawner;
            ResetUI();
            restartButton.onClick.AddListener(ResetGame);
        }

        public void UpdateScore(int score)
        {
            scoreText.text = $"Score: {score} / {_enemySpawner.totalSpawnCount}";
        }

        public void ShowLosePanel()
        {
            _losePanel.SetActive(true);
            ShowRestartButton();
        }

        public void ShowWinPanel()
        {
            _winPanel.SetActive(true);
            ShowRestartButton();
        }

        public void ShowRestartButton()
        {
            restartButton.gameObject.SetActive(true);
        }

        public void ResetUI()
        {
            _losePanel.SetActive(false);
            _winPanel.SetActive(false);
            restartButton.gameObject.SetActive(false);
            UpdateScore(0);
        }

        private void ResetGame()
        {
            SingletonContainer.Instance.OpGameManager._restartGame?.Invoke();
        }
    }
}
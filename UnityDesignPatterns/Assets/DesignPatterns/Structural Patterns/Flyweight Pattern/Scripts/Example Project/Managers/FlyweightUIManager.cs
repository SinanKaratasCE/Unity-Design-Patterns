using Zenject;
using UnityEngine;
using TMPro;
using Utils;
using UnityEngine.UI;

namespace DesignPatterns.Flyweight
{
    public class FlyweightUIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text remainingTimeText;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private GameObject losePanel, winPanel;
        [SerializeField] private Button restartButton;
        private FlyweightGameManager _flyweightGameManager;

        [Inject]
        private void OnInstaller(FlyweightGameManager flyweightGameManager)
        {
            _flyweightGameManager = flyweightGameManager;
        }

        private void OnEnable()
        {
            _flyweightGameManager.ScoreEvent += UpdateScoreText;
        }

        private void OnDisable()
        {
            _flyweightGameManager.ScoreEvent -= UpdateScoreText;
        }

        public void UpdateTimeText()
        {
            remainingTimeText.text = $"Remaining Time: {(int)_flyweightGameManager.RemainingTime}";
        }

        public void UpdateScoreText()
        {
            scoreText.text = $"Score: {_flyweightGameManager.PlayerScore}";
        }

        private void ActivateRestartButton()
        {
            restartButton.gameObject.SetActive(true);
            restartButton.onClick.AddListener(() =>
            {
                _flyweightGameManager.RestartGame();
                restartButton.gameObject.SetActive(false);
            });
        }

        public void GameStatePanel(bool condition)
        {
            if (condition)
            {
                Timer.Instance.TimerWait(1f, () =>
                {
                    winPanel.SetActive(true);
                    ActivateRestartButton();
                });
            }
            else
            {
                Timer.Instance.TimerWait(1f, () =>
                {
                    losePanel.SetActive(true);
                    ActivateRestartButton();
                });
            }
        }

        public void CloseGameStatePanels()
        {
            winPanel.SetActive(false);
            losePanel.SetActive(false);
        }
    }
}
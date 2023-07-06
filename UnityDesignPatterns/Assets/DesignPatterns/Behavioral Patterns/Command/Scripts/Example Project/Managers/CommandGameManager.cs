using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace DesignPatterns.Command
{
    public class CommandGameManager : MonoBehaviour
    {
        private int _remainingMoves;
        private const int MaxMoveCount = 20;
        [SerializeField] private TMP_Text remainingMoveCountText;
        [SerializeField] private GameObject winEffect;

        public int RemainingMoves
        {
            get => _remainingMoves;
            set
            {
                if (value is < 0 or > MaxMoveCount)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, "Move count must be between 0 and 20");
                }
                else
                {
                    _remainingMoves = value;
                    Debug.Log($"Remaining move count: {_remainingMoves}");
                }
            }
        }

        private void Start()
        {
            _remainingMoves = MaxMoveCount;
            UpdateMoveCountText();
        }

        public void DecreaseMoveCount()
        {
            RemainingMoves--;
            UpdateMoveCountText();
        }

        public void IncreaseMoveCount()
        {
            RemainingMoves++;
            UpdateMoveCountText();
        }

        private void UpdateMoveCountText()
        {
            remainingMoveCountText.text = $"{RemainingMoves} Moves Left";
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        public void LevelComplete()
        {
            winEffect.SetActive(true);
        }
    }
}
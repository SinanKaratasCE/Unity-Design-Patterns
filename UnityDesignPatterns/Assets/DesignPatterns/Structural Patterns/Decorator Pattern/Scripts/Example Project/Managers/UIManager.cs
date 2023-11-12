using System;
using System.Collections.Generic;
using Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns.Decorator
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text moneyText;
        [SerializeField] private GameObject itemShopUI;
        [SerializeField] private GameObject buyDecisionPanel;
        [SerializeField] private GameObject insufficientMoneyText;
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private Button restartGameButton;
        [SerializeField] private Player player;
        public PlayerStatsUI playerStatsUI;
        private GameManager _gameManager;


        private void Awake()
        {
            _gameManager = SingletonContainer.Instance.GameManager;
        }

        private void Start()
        {
            restartGameButton.onClick.AddListener(_gameManager.RestartGame);
        }

        public void UpdateGoldText(int money)
        {
            moneyText.text = $"Money: {money}";
        }

        public void OpenItemShopUI()
        {
            //Open Item Shop UI
            itemShopUI.SetActive(true);
        }

        public void CloseItemShopUI()
        {
            //Close Item Shop UI
            itemShopUI.SetActive(false);
            player.CanDig = true;
        }

        public void ShowDecisionPanel()
        {
            buyDecisionPanel.SetActive(true);
        }
        public void CloseDecisionPanel()
        {
            buyDecisionPanel.SetActive(false);
        }

        public void ShowInsufficientMoneyText()
        {
            if(insufficientMoneyText.activeSelf) return;
            insufficientMoneyText.SetActive(true);
            Timer.Instance.TimerWait(2f, () => insufficientMoneyText.SetActive(false));
        }
        
        public void ShowGameOverPanel()
        {
            gameOverPanel.SetActive(true);
        }

        private void HideGameOverPanel()
        {
            gameOverPanel.SetActive(false);
        }

        public void ResetGameUI()
        {
            UpdateGoldText(0);
            HideGameOverPanel();
        }
        
        

    }
}
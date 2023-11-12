using System;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Decorator
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private List<PreciousStone> stonesList = new List<PreciousStone>();
        private UIManager _uiManager;


        private void Start()
        {
            _uiManager = SingletonContainer.Instance.UIManager;
        }

        public void GameOver()
        {
            _uiManager.ShowGameOverPanel();
        }

        public void RestartGame()
        {
            player.ResetPlayerProperties();
            _uiManager.ResetGameUI();
            ResetAndActivateStones();
        }

        private void ResetAndActivateStones()
        {
            foreach (var stone in stonesList)
            {
                stone.ResetStone();
            }
        }
    }
}
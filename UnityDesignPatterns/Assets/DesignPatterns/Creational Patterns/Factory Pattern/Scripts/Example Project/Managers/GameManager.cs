using System;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Factory
{
    public class GameManager : MonoBehaviour
    {
        #region Private References

        private WishlistManager _wishlistManager;
        private ContainerShip _containerShip;
        private FactoryManager _factoryManager;
        private UIManager _uiManager;

        #endregion

        private void Start()
        {
            InstallReferences();
        }

        private void InstallReferences()
        {
            _wishlistManager = SingletonContainer.Instance.WishlistManager;
            _containerShip = SingletonContainer.Instance.ContainerShip;
            _factoryManager = SingletonContainer.Instance.FactoryManager;
            _uiManager = SingletonContainer.Instance.UIManager;
        }

        public void CheckWishlist()
        {
            _wishlistManager.CheckWishlistAgainstContainerShip(_containerShip.loadedWeapons);
        }

        public void GameOver()
        {
            Debug.Log("Game Over");
            _uiManager.GameOver();
        }

        public void GameWon()
        {
            Debug.Log("Game Won");
            _uiManager.GameWon();
        }

        public void RestartGame()
        {
            _containerShip.ClearContainerShip();
            _factoryManager.ResetFactories();
            _uiManager.ResetUI();
            _wishlistManager.ResetWishlist();
        }
    }
}
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace DesignPatterns.Factory
{
    public class UIManager : MonoBehaviour
    {
        #region Scene References

        [SerializeField] private GameObject wishlistIconsParent;
        [SerializeField] private GameObject containerShipIconsParent;
        [SerializeField] private GameObject weaponIconPrefab;
        [SerializeField] private Button productsReadyButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private GameObject gameWonPanel;

        #endregion

        #region Private References

        private GameManager _gameManager;
        private ContainerShip _containerShip;
        private List<GameObject> _allIcons = new List<GameObject>();

        #endregion


        private void Start()
        {
            InstallReferences();
            AddButtonListeners();
        }

        private void InstallReferences()
        {
            _gameManager = SingletonContainer.Instance.GameManager;
            _containerShip = SingletonContainer.Instance.ContainerShip;
        }

        private void AddButtonListeners()
        {
            productsReadyButton.onClick.AddListener(() => { _containerShip.StartTransport(); });
            restartButton.onClick.AddListener(() => { _gameManager.RestartGame(); });
        }

        public void AddToWishlist(WeaponStats weapon)
        {
            GameObject instance = Instantiate(weaponIconPrefab, wishlistIconsParent.transform);
            instance.GetComponent<Image>().sprite = weapon.icon;
            instance.transform.SetParent(wishlistIconsParent.transform);
            _allIcons.Add(instance);
        }

        public void AddToContainerShip(IWeapon weapon)
        {
            GameObject instance = Instantiate(weaponIconPrefab, containerShipIconsParent.transform);
            instance.GetComponent<Image>().sprite = weapon.Icon;
            instance.transform.SetParent(containerShipIconsParent.transform);
            _allIcons.Add(instance);
        }

        public void ClearAllIcons()
        {
            foreach (var icon in _allIcons)
            {
                Destroy(icon);
            }
        }

        private void CloseAllPanels()
        {
            gameOverPanel.SetActive(false);
            gameWonPanel.SetActive(false);
            restartButton.gameObject.SetActive(false);
        }

        public void ResetUI()
        {
            CloseAllPanels();
            ClearAllIcons();
        }

        public void GameOver()
        {
            gameOverPanel.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }

        public void GameWon()
        {
            gameWonPanel.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }
    }
}
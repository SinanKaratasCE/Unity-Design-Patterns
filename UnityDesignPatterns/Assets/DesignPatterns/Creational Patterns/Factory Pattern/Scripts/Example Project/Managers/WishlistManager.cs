using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace DesignPatterns.Factory
{
    public class WishlistManager : MonoBehaviour
    {
        #region Private References

        [SerializeField] private List<WeaponStats> weaponList = new List<WeaponStats>();
        private List<WeaponStats> wishlist = new List<WeaponStats>();
        private UIManager _uiManager;
        private GameManager _gameManager;

        #endregion


        private void Start()
        {
            InstallReferences();
            GetRandomNumberOfRandomWeapons();
        }
        
        private void InstallReferences()
        {
            _uiManager = SingletonContainer.Instance.UIManager;
            _gameManager = SingletonContainer.Instance.GameManager;
        }

        public void CheckWishlistAgainstContainerShip(List<IWeapon> containerShipWeapons)
        {
            if (containerShipWeapons.Count > wishlist.Count)
            {
                _gameManager.GameOver();
                return;
            }

            for (int i = 0; i < wishlist.Count; i++)
            {
                for (int j = 0; j < containerShipWeapons.Count; j++)
                {
                    if (wishlist[i].weaponName == containerShipWeapons[j].WeaponName)
                    {
                        wishlist.RemoveAt(i);
                        i--;
                        break;
                    }
                }
            }

            IsWishlistProvided();
        }

        private void IsWishlistProvided()
        {
            if (wishlist.Count == 0)
            {
                _gameManager.GameWon();
            }
            else
            {
                _gameManager.GameOver();
            }
        }

        private void GetRandomNumberOfRandomWeapons()
        {
            int randomNumberOfWeapons = Random.Range(1, 7);
            for (int i = 0; i < randomNumberOfWeapons; i++)
            {
                int randomIndex = Random.Range(0, weaponList.Count);
                wishlist.Add(weaponList[randomIndex]);
                _uiManager.AddToWishlist(weaponList[randomIndex]);
            }
        }

        public void ResetWishlist()
        {
            wishlist.Clear();
            GetRandomNumberOfRandomWeapons();
        }
    }
}
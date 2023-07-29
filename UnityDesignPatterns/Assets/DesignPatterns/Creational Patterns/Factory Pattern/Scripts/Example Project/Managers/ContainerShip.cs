using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace DesignPatterns.Factory
{
    public class ContainerShip : MonoBehaviour
    {
        public int MaxCapacity { get; } = 6;

        #region Private References

        private Transform[] weaponLocations = new Transform[6];
        private UIManager _uiManager;
        private GameManager _gameManager;
        private FactoryManager _factoryManager;
        private Animator _animator;

        #endregion


        [Header("Public References")] public List<IWeapon> loadedWeapons = new List<IWeapon>();

        private List<GameObject> _weaponsInShip = new List<GameObject>();
        private bool _canTransport = true;

        private void Awake()
        {
            GetWeaponLocations();
        }

        private void Start()
        {
            InstallReferences();
        }
        
        private void InstallReferences()
        {
            _uiManager = SingletonContainer.Instance.UIManager;
            _gameManager = SingletonContainer.Instance.GameManager;
            _factoryManager = SingletonContainer.Instance.FactoryManager;
            _animator = GetComponent<Animator>();
        }

        public void AddWeaponToContainerShip(IWeapon weapon)
        {
            if (loadedWeapons.Count < MaxCapacity)
            {
                loadedWeapons.Add(weapon);
                _uiManager.AddToContainerShip(weapon);
                Debug.Log(
                    $"Added {weapon.WeaponName} to the container ship.");
            }
            else
            {
                Debug.Log($"The container ship is full. Cannot add {weapon.WeaponName}.");
            }
        }

        public void LoadWeaponsIntoProperLocations(Transform weaponTransform)
        {
            weaponTransform.SetParent(weaponLocations[loadedWeapons.Count - 1]);
            weaponTransform.position = weaponLocations[loadedWeapons.Count - 1].position;
            _weaponsInShip.Add(weaponTransform.gameObject);
        }

        private void GetWeaponLocations()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                weaponLocations[i] = transform.GetChild(i);
            }
        }

        public void StartTransport()
        {
            if (_factoryManager.ProducedWeaponsCount != loadedWeapons.Count || !_canTransport)
                return;
            _canTransport = false;
            _animator.SetTrigger("StartTransport");
        }

        private void CheckWishlist()
        {
            _gameManager.CheckWishlist();
        }

        public void ClearContainerShip()
        {
            foreach (var weapon in _weaponsInShip)
            {
                Destroy(weapon);
            }

            _canTransport = true;
            loadedWeapons.Clear();
            _weaponsInShip.Clear();
            _uiManager.ClearAllIcons();
        }
    }
}
using UnityEngine;

namespace DesignPatterns.Decorator
{
    public class SingletonContainer : MonoBehaviour
    {
        public static SingletonContainer Instance { get; private set; }

        #region Manager Properties

        public UIManager UIManager { get; private set; }
        public ItemShop ItemShop { get; private set; }
        public GameManager GameManager { get; private set; }
        public VFXSpawner VFXSpawner { get; private set; }

        #endregion


        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }

            Instance = this;
            InitializeSingletonReferences();
        }

        private void InitializeSingletonReferences()
        {
            UIManager = GetComponentInChildren<UIManager>();
            ItemShop = GetComponentInChildren<ItemShop>();
            GameManager = GetComponentInChildren<GameManager>();
            VFXSpawner = GetComponentInChildren<VFXSpawner>();
        }
    }
}
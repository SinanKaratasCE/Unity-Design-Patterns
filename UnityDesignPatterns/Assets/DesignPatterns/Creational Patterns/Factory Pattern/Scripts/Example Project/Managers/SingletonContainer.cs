using UnityEngine;

namespace DesignPatterns.Factory
{
    public class SingletonContainer : MonoBehaviour
    {
        public static SingletonContainer Instance { get; private set; }

        #region Manager Properties

        public FactoryManager FactoryManager { get; private set; }
        public ContainerShip ContainerShip { get; private set; }
        public WishlistManager WishlistManager { get; private set; }
        public UIManager UIManager { get; private set; }
        public GameManager GameManager { get; private set; }

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
            FactoryManager = GetComponentInChildren<FactoryManager>();
            ContainerShip = GetComponentInChildren<ContainerShip>();
            WishlistManager = GetComponentInChildren<WishlistManager>();
            UIManager = GetComponentInChildren<UIManager>();
            GameManager = GetComponentInChildren<GameManager>();
        }
    }
}
using UnityEngine;

namespace DesignPatterns.SingletonObjectPool
{
    public class SingletonContainer : MonoBehaviour
    {
        //This container is used to hold all the singletons in the game.
        //This is useful for when you want to have a single point of access to all the singletons in the game.

        public static SingletonContainer Instance { get; private set; }
        public GameManager GameManager { get; private set; }
        public EnemySpawner EnemySpawner { get; private set; }

        public UIManager OpUIManager { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }

            Instance = this;
            GameManager = GetComponentInChildren<GameManager>();
            OpUIManager = GetComponentInChildren<UIManager>();
            EnemySpawner = GetComponentInChildren<EnemySpawner>();
        }
    }
}
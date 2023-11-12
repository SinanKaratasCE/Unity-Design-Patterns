using UnityEngine;

namespace DesignPatterns.SingletonObjectPool
{
    public class SimpleSingleton : MonoBehaviour
    {
        // Global access
        public static SimpleSingleton Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                // If Instance is already set, destroy this duplicate
                Destroy(gameObject);
            }
            else
            {
                // If Instance is not set, make this instance the singleton
                Instance = this;
            }
        }
    }
}
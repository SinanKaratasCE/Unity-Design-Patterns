using UnityEngine;
using Zenject;

namespace DesignPatterns.Flyweight
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        // **We can use the same EnemyStats for all enemies and this way we can save a lot of memory
        [SerializeField] private EnemyStats enemyStats;

        private float _currentHealth;
        private FlyweightGameManager _flyweightGameManager;


        public void SetFlyweightGameManager(FlyweightGameManager flyweightGameManager)
        {
            _flyweightGameManager = flyweightGameManager;
        }

        private void OnEnable()
        {
            _currentHealth = Random.Range(enemyStats.minHealth, enemyStats.maxHealth);
        }

        public void ApplyDamage(float damage)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                DisableEnemy();
            }
        }

        private void DisableEnemy()
        {
            _flyweightGameManager.ScoreEventInitializer();
            gameObject.SetActive(false);
        }
    }
}
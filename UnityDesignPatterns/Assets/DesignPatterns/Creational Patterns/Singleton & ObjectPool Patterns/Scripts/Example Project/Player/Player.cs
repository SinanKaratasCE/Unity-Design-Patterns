using System;
using UnityEngine;

namespace DesignPatterns.SingletonObjectPool
{
    public class Player : MonoBehaviour, IDamageable
    {
        [SerializeField] private CharacterStatsSO _characterStats;
        private float _health = 100f;
        private float _damage = 10f;
        private bool _isDead = false;
        private GameManager _gameManager;

        public float CollisionDamage { get; set; } = 100f;
        public bool IsDead => _isDead;


        public float Health
        {
            get => _health;
            set
            {
                if (_health > 0)
                {
                    _health = value;
                }
            }
        }

        private void OnDisable()
        {
            _gameManager._restartGame -= Reset;
        }

        private void Start()
        {
            _health = _characterStats.Health;
            _damage = _characterStats.Damage;
            _gameManager = SingletonContainer.Instance.GameManager;
            _gameManager._restartGame += Reset;
        }

        public void TakeDamage(float damage)
        {
            if (_isDead)
            {
                return;
            }

            Health -= damage;
            CheckHealth();
        }

        private void CheckHealth()
        {
            if (Health <= 0)
            {
                _gameManager.GameOver();
            }
        }

        public void Reset()
        {
            _isDead = false;
            Health = _characterStats.Health;
        }
    }
}
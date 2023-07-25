using System;
using UnityEngine;
using DesignPatterns.SingletonObjectPool;

namespace DesignPatterns.Strategy
{
    public class Castle : MonoBehaviour, IDamageable
    {
        private float _health = 100;
        public HealthBar healthBar;

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

        // Example usage
        void Start()
        {
            // Get the HealthBar component attached to the player
            healthBar = GetComponent<HealthBar>();
        }

        public void TakeDamage(float damageAmount)
        {
            healthBar.TakeDamage(damageAmount);
        }

        public void ResetHealth()
        {
            healthBar.ResetHealthBar();
        }
    }
}
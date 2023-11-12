using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DesignPatterns.Strategy
{
    public class HealthBar : MonoBehaviour
    {
        private GameManager _gameManager;
        public Slider healthSlider;
        public float maxHealth = 100f;
        public float currentHealth = 100f;

        [Inject]
        private void OnIntaller(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        void Start()
        {
            // Initialize the health bar with full health
            ResetHealthBar();
        }

        public void TakeDamage(float damageAmount)
        {
            currentHealth -= damageAmount;
            currentHealth =
                Mathf.Clamp(currentHealth, 0f, maxHealth); // Ensure health stays within 0 to maxHealth range

            UpdateHealthBar();

            if (currentHealth <= 0f)
            {
                _gameManager.GameOver();
            }
        }

        public void Heal(float healAmount)
        {
            currentHealth += healAmount;
            currentHealth =
                Mathf.Clamp(currentHealth, 0f, maxHealth); // Ensure health stays within 0 to maxHealth range

            UpdateHealthBar();
        }

        private void UpdateHealthBar()
        {
            // Update the UI Slider value to reflect the current health
            healthSlider.value = currentHealth / maxHealth;
        }
        
        public void ResetHealthBar()
        {
            currentHealth = maxHealth;
            UpdateHealthBar();
        }
    }
}
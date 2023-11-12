using UnityEngine;

namespace DesignPatterns.Flyweight
{
    public class UnrefactoredEnemy : MonoBehaviour
    {
        // This way we create a new instance of all enemy stats for each enemy and lose a lot of memory
        public float maxHealth;
        public float minHealth;
        public float speed;
        public float damage;
        public float attackRange;
        public float currentHealth;
        public float currentDamage;
        public float currentSpeed;
    }
}
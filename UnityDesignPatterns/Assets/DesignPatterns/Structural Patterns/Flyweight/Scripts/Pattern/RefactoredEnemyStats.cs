using UnityEngine;

namespace DesignPatterns.Flyweight
{
    public class RefactoredEnemyStats : MonoBehaviour
    {
        /* --We can use the same Enemy Stats from a constant location for all enemies
         and this way we can save a lot of memory                                   */

        public float maxHealth;
        public float minHealth;
        public float speed;
        public float damage;
        public float attackRange;
    }
}
using UnityEngine;

namespace DesignPatterns.Flyweight
{
    [CreateAssetMenu(fileName = "EnemyStats", menuName = "ScriptableObjects/CharacterStats", order = 1)]
    public class EnemyStats : ScriptableObject
    {
        public float maxHealth;
        public float minHealth;
        public float speed;
        public float damage;
        public float attackRange;
    }
}
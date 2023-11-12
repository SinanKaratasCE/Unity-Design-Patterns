using UnityEngine;

namespace DesignPatterns.Flyweight
{
    public class RefactoredEnemy : MonoBehaviour
    {
        //Only one instance of RefactoredEnemyStats is needed for all enemies
        private RefactoredEnemyStats _enemyStats;
        private float _currentHealth;
        private float _currentDamage;
        private float _currentSpeed;
    }
}
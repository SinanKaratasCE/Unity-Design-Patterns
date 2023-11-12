using UnityEngine;

namespace DesignPatterns.SingletonObjectPool
{
    public interface IDamageable
    {
        float Health { get; set; }
        void TakeDamage(float damage);
    }
}
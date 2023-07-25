using UnityEngine;
using DesignPatterns.SingletonObjectPool;

namespace DesignPatterns.Strategy
{

    public class PoisonDamage : IDoDamage
    {
        public void DoDamage(IDamageable target, float damage)
        {
          target.TakeDamage(damage);
        }
    }

}
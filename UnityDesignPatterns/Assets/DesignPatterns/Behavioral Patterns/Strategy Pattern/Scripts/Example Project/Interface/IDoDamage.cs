using UnityEngine;
using DesignPatterns.SingletonObjectPool;

namespace DesignPatterns.Strategy
{
    public interface IDoDamage
    {
        void DoDamage(IDamageable target,float damage);
    }
}
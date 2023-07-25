using DesignPatterns.SingletonObjectPool;
using UnityEngine;

namespace DesignPatterns.Strategy
{
    public class SpellBase : ProjectileBase
    {
        private IDoDamage _damageType;

        public void SetDamageType(IDoDamage damageType)
        {
            _damageType = damageType;
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out IDamageable damageable)) return;
            if (other.GetComponent<Enemy>().weakness.GetType() != _damageType.GetType())
            {
                _damageType.DoDamage(damageable, SpellDamage * 2);
            }

            gameObject.SetActive(false);
        }
    }
}
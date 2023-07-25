using System;
using UnityEngine;
using System.Collections;
using DesignPatterns.Strategy;

namespace DesignPatterns.SingletonObjectPool
{
    public class ProjectileBase : MonoBehaviour
    {
        // deactivate after delay
        [SerializeField] private float timeoutDelay = 1f;
        private float spellDamage = 50f;

        public float SpellDamage
        {
            get => spellDamage;
            set => spellDamage = value;
        }


        public void Deactivate()
        {
            StartCoroutine(DeactivateRoutine(timeoutDelay));
        }

        IEnumerator DeactivateRoutine(float delay)
        {
            yield return new WaitForSeconds(delay);

            gameObject.SetActive(false);
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out IDamageable damageable)) return;
            Debug.Log($"OnTrigger 2 kere çalıştı");
            damageable.TakeDamage(spellDamage);
            gameObject.SetActive(false);
        }
    }
}
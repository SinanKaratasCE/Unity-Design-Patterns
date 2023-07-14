using System;
using UnityEngine;
using System.Collections;

namespace DesignPatterns.SingletonObjectPool
{
    public class Projectile : MonoBehaviour
    {
        // deactivate after delay
        [SerializeField] private float timeoutDelay = 1f;
        [SerializeField] private float spellDamage = 50f;


        public void Deactivate()
        {
            StartCoroutine(DeactivateRoutine(timeoutDelay));
        }

        IEnumerator DeactivateRoutine(float delay)
        {
            yield return new WaitForSeconds(delay);

            // reset the moving Rigidbody
            // Rigidbody rBody = GetComponent<Rigidbody>();
            // rBody.velocity = new Vector3(0f, 0f, 0f);
            // rBody.angularVelocity = new Vector3(0f, 0f, 0f);

            gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out IDamageable damageable)) return;
            damageable.TakeDamage(spellDamage);
            gameObject.SetActive(false);
        }
    }
}
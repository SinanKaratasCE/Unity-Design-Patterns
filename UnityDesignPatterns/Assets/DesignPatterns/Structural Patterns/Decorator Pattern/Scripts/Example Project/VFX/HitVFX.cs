using System;
using System.Collections;
using UnityEngine;

namespace DesignPatterns.Decorator
{
    public class HitVFX : MonoBehaviour
    {
        // deactivate after delay
        [SerializeField] private float timeoutDelay = 1f;

        private void OnEnable()
        {
            Deactivate();
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
    }
}
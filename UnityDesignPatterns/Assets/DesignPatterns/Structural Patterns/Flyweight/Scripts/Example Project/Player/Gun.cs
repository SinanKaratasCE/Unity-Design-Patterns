using UnityEngine;

namespace DesignPatterns.Flyweight
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private LineRenderer beam;
        [SerializeField] private Transform firePoint;
        [SerializeField] private float damage;
        [SerializeField] private float fireRange;
        [SerializeField] private LayerMask layerMask;

        


        private void Awake()
        {
            beam.enabled = false;
        }

        private void ActivateBeam()
        {
            beam.enabled = true;
        }

        private void DeactivateBeam()
        {
            beam.enabled = false;
            SetBeamPosition(firePoint.position);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                ActivateBeam();
            else if (Input.GetMouseButtonUp(0))
                DeactivateBeam();
        }

        private void FixedUpdate()
        {
            if (!beam.enabled) return;

            if (Physics.Raycast(firePoint.position, firePoint.forward, out var hit, fireRange, layerMask))
            {
                SetBeamPosition(hit.transform.position);
                if (hit.collider.TryGetComponent(out IDamageable enemy))
                {
                    enemy.ApplyDamage(damage * Time.fixedDeltaTime);
                }
            }
            else
            {
                Vector3 hitPosition = firePoint.forward * fireRange;
                SetBeamPosition(hitPosition);
            }
        }

        private void SetBeamPosition(Vector3 hitPoint)
        {
            beam.SetPosition(0, Vector3.zero);
            beam.SetPosition(1, hitPoint);
        }
    }
}
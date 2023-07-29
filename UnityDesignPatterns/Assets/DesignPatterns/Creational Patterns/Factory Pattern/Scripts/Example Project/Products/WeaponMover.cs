using UnityEngine;

namespace DesignPatterns.Factory
{
    public class WeaponMover : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            // Move the character forward
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }
    }
}
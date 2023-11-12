using System;
using UnityEngine;

namespace DesignPatterns.Flyweight
{
    public class PlayerController : MonoBehaviour
    {
        private Camera _mainCamera;

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            // Cast a ray from the mouse position into the scene
            Ray mouseRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits the ground plane
            if (Physics.Raycast(mouseRay, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            {
                // Get the direction from the character's position to the mouse hit point
                Vector3 direction = hit.point - transform.position;
                direction.y = 0f; // Set the y-component to zero to keep the character level

                // Rotate the character to face the mouse hit point
                if (direction != Vector3.zero)
                {
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    transform.rotation = lookRotation;
                }
            }
        }
    }
}
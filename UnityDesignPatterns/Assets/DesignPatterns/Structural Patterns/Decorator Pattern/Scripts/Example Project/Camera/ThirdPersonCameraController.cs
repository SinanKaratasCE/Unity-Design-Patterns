using System;
using UnityEngine;

namespace DesignPatterns.Decorator
{
    public class ThirdPersonCameraController : MonoBehaviour
    {
        public Transform target; // The target object (your character)
        public float distance = 5.0f; // Distance from the target
        public float height = 2.0f; // Height above the target
        public float rotationDamping = 3.0f; // Speed of camera rotation
        
   
        void LateUpdate()
        {
            if (!target)
                return;

            // Calculate the desired rotation angle and position
            float wantedRotationAngle = target.eulerAngles.y;
            float wantedHeight = target.position.y + height;

            float currentRotationAngle = transform.eulerAngles.y;
            float currentHeight = transform.position.y; // Corrected line

            // Damping for rotation
            currentRotationAngle =
                Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

            Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

            // Set the position of the camera on the x-z plane
            transform.position = target.position;
            transform.position -= currentRotation * Vector3.forward * distance;

            // Set the height of the camera
            Vector3 tempPos = transform.position;
            tempPos.y = currentHeight;
            transform.position = tempPos;

            // Make the camera look at the target
            transform.LookAt(target);
        }
    }
}
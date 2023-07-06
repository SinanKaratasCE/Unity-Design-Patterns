using UnityEngine;

namespace Utils
{
    public class LookAtCamera : MonoBehaviour
    {
        private Transform targetCamera;

        private void Start()
        {
            targetCamera = Camera.main.transform;
        }

        private void Update()
        {
            LookAtToMainCamera();
        }

        public void LookAtToMainCamera()
        {
            transform.LookAt(targetCamera);
        }
    }
}
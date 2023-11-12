using UnityEngine;

namespace DesignPatterns.Decorator
{
    public class LookAtCamera : MonoBehaviour
    {
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            transform.rotation = Quaternion.LookRotation(transform.position - _camera.transform.position);
        }
    }
}
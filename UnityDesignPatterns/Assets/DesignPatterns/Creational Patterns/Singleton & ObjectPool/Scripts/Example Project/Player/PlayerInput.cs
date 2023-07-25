using UnityEngine;

namespace DesignPatterns.SingletonObjectPool
{
    public class PlayerInput : MonoBehaviour
    {
        [Header("Controls")] [SerializeField] private KeyCode forward = KeyCode.W;
        [SerializeField] private KeyCode back = KeyCode.S;
        [SerializeField] private KeyCode left = KeyCode.A;
        [SerializeField] private KeyCode right = KeyCode.D;

        public Vector3 InputVector => inputVector;

        private Vector3 inputVector;
        private bool isJumping;
        private float xInput;
        private float zInput;
        private float yInput;

        public void HandleInput()
        {
            xInput = 0;
            yInput = 0;
            zInput = 0;

            if (Input.GetKey(forward))
            {
                zInput++;
            }

            if (Input.GetKey(back))
            {
                zInput--;
            }

            if (Input.GetKey(left))
            {
                xInput--;
            }

            if (Input.GetKey(right))
            {
                xInput++;
            }

            inputVector = new Vector3(xInput, yInput, zInput);
        }

        private void Update()
        {
            HandleInput();
        }
    }
}
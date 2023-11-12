using UnityEngine;

namespace DesignPatterns.Decorator
{
    public class PlayerInput : MonoBehaviour
    {
        [Header("Controls")] [SerializeField] private KeyCode forward = KeyCode.W;
        [SerializeField] private KeyCode back = KeyCode.S;
        [SerializeField] private KeyCode left = KeyCode.A;
        [SerializeField] private KeyCode right = KeyCode.D;
        [SerializeField] private KeyCode attack = KeyCode.Mouse0;

        public Vector3 InputVector => inputVector;

        public bool IsDiggingInput
        {
            get => _isDiggingInput;
            set => _isDiggingInput = value;
        }

        private Vector3 inputVector;
        private bool _isDiggingInput;
        private float xInput;
        private float zInput;
        private float yInput;

        private void HandleInput()
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

            _isDiggingInput = Input.GetKeyDown(attack);
        }

        private void Update()
        {
            HandleInput();
        }
    }
}
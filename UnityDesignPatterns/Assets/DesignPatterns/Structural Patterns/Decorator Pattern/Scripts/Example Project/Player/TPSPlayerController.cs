using UnityEngine;
using Zenject.SpaceFighter;
using Utils;

namespace DesignPatterns.Decorator
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Animator))]
    public class TPSPlayerController : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 500.0f;
        public bool CanMove { get; set; } = true;
        public float MoveSpeed { get; set; } = 5.0f;

        private const float Gravity = 1000.0f;

        private CharacterController _controller;
        private Animator _animator;
        private PlayerInput _playerInput;

        [Header("Character Movement Variables")]
        private Vector3 _inputVector;

        private Vector3 _forward;
        private Vector3 _right;
        private Vector3 _moveDirection;


        void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();
            _playerInput = GetComponent<PlayerInput>();
        }

        void Update()
        {
            if (!CanMove)
            {
                return;
            }

            // Handle character movement
            _inputVector = _playerInput.InputVector;

            _forward = transform.forward * _inputVector.z;
            _right = transform.right * _inputVector.x;

            _moveDirection = (_forward + _right).normalized;
            _moveDirection *= MoveSpeed;

            AnimateCharacterBySpeed(_inputVector.x, _inputVector.z);

            if (!_controller.isGrounded)
            {
                _moveDirection.y -= Gravity * Time.deltaTime;
            }

            _controller.Move(_moveDirection * Time.deltaTime);

            //Lock and unlock cursor with right mouse button
            if (Input.GetMouseButtonDown(1)) // Right mouse button down
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            if (Input.GetMouseButtonUp(1)) // Right mouse button up
            {
                Cursor.lockState = CursorLockMode.None;
            }

            if (Input.GetMouseButton(1))
            {
                // Handle character rotation based on mouse input
                float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
                transform.Rotate(Vector3.up * mouseX, Space.World);
            }
        }

        private void AnimateCharacterBySpeed(float horizontalSpeed, float verticalSpeed)
        {
            _animator.SetFloat("x", horizontalSpeed);
            _animator.SetFloat("y", verticalSpeed);
        }

        public void ResetPlayerPosition()
        {
            CanMove = false;
            transform.position = Vector3.zero;

            Timer.Instance.TimerWait(2f, () => { CanMove = true; });
        }
    }
}
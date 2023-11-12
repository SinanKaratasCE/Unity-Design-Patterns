using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;


namespace DesignPatterns.Strategy
{
    [RequireComponent(typeof(CharacterController))]
    public class FPSPlayerController : MonoBehaviour
    {
        public float walkingSpeed = 7.5f;
        public float runningSpeed = 11.5f;
        public float jumpSpeed = 8.0f;
        public float gravity = 20.0f;
        public Camera playerCamera;
        public float lookSpeed = 2.0f;
        public float lookXLimit = 45.0f;

        CharacterController characterController;
        Vector3 moveDirection = Vector3.zero;
        private Animator _animator;
        private PlayerInput _playerInput;
        float rotationX = 0;
        private Vector3 _inputVector;
        private Vector3 _forward;
        private Vector3 _right;

        [HideInInspector] public bool canMove = true;
        private WandChanger wandChanger;

        void Start()
        {
            characterController = GetComponent<CharacterController>();
            wandChanger = GetComponent<WandChanger>();
            _animator = GetComponent<Animator>();
            _playerInput = GetComponent<PlayerInput>();
        }

        void Update()
        {
            _inputVector = _playerInput.InputVector;
            _forward = transform.TransformDirection(Vector3.forward);
            _right = transform.TransformDirection(Vector3.right);

            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * _inputVector.z : 0;
            float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * _inputVector.x : 0;
            float movementDirectionY = moveDirection.y;
            moveDirection = (_forward * curSpeedX) + (_right * curSpeedY);

            if (_playerInput.IsJumping && canMove && characterController.isGrounded)
            {
                moveDirection.y = jumpSpeed;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }


            if (!characterController.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }

            characterController.Move(moveDirection * Time.deltaTime);

            if (canMove)
            {
                rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            }
        }

        public void ChangePlayerStateAttackToIdle()
        {
            _animator.SetBool(Strings.AttackAnimation, false);
            wandChanger.canChange = true;
        }
    }
}
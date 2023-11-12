using System;
using UnityEngine;
using Utils;

namespace DesignPatterns.SingletonObjectPool
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")] [Tooltip("Horizontal speed")] [SerializeField]
        private float moveSpeed = 5f;

        [Tooltip("Rate of change for move speed")] [SerializeField]
        private float acceleration = 10f;
        
        [SerializeField] private bool isGrounded = true;

        private float targetSpeed;
        private float verticalVelocity;
        private float jumpCooldown;

        #region Properties

        public CharacterController CharacterController => _characterController;
        public PlayerInput PlayerInput => _playerInput;
        public bool IsGrounded => isGrounded;

        #endregion

        [Header("Other Components")] private CharacterController _characterController;
        private PlayerInput _playerInput;
        private Animator _animator;
        private Camera _mainCamera;
        private float forwardSpeed;
        private float sideSpeed;


        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _characterController = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            SetCharacterRotation();
        }

        private void SetCharacterRotation()
        {
            Ray mouseRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(mouseRay, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            {
                Vector3 direction = hit.point - transform.position;
                direction.y = 0f;

                if (direction != Vector3.zero)
                {
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    transform.rotation = lookRotation;
                }
            }
        }


        private void LateUpdate()
        {
            Move();
        }

        private void Move()
        {
            Vector3 inputVector = _playerInput.InputVector;

            if (inputVector == Vector3.zero)
            {
                targetSpeed = 0;
            }

            AnimationState(inputVector);

            // if we are not at target speed (outside of tolerance), lerp to the target speed
            float currentHorizontalSpeed =
                new Vector3(_characterController.velocity.x, 0.0f, _characterController.velocity.z).magnitude;
            float tolerance = 0.1f;


            if (currentHorizontalSpeed < targetSpeed - tolerance || currentHorizontalSpeed > targetSpeed + tolerance)
            {
                targetSpeed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed, Time.deltaTime * acceleration);
                targetSpeed = Mathf.Round(targetSpeed * 1000f) / 1000f;
            }
            else
            {
                targetSpeed = moveSpeed;
            }

            _characterController.Move((inputVector.normalized * targetSpeed * Time.deltaTime) +
                                      new Vector3(0f, verticalVelocity, 0f) * Time.deltaTime);
        }


        public void Attack()
        {
            _animator.SetTrigger("Attack");
        }

        public void ChangePlayerStateAttackToIdle()
        {
            _animator.SetBool(Strings.AttackAnimation, false);
        }

        private void AnimationState(Vector3 inputVector)
        {
            // change animation based on character direction and speed
            forwardSpeed = (inputVector.x * transform.forward.x) + (inputVector.z * transform.forward.z);
            sideSpeed = (inputVector.z * transform.right.z) + (inputVector.x * transform.right.x);
            if (Mathf.Abs(forwardSpeed) > Mathf.Abs(sideSpeed))
            {
                _animator.SetFloat("Compare", 1);
            }
            else
            {
                _animator.SetFloat("Compare", 0);
            }

            _animator.SetFloat("Speed", forwardSpeed);
            _animator.SetFloat("SideSpeed", sideSpeed);
        }
    }
}
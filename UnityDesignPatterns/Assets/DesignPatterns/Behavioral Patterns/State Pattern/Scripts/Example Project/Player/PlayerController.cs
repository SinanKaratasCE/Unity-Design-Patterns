using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace DesignPatterns.State
{
    [RequireComponent(typeof(PlayerInput), typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")] [Tooltip("Horizontal speed")] [SerializeField]
        private float moveSpeed = 5f;

        [Tooltip("Rate of change for move speed")] [SerializeField]
        private float acceleration = 10f;

        [Tooltip("Max height to jump")] [SerializeField]
        private float jumpHeight = 1.25f;

        [Tooltip("Custom gravity for player")] [SerializeField]
        private float gravity = -15f;

        [Tooltip("Time between jumps")] [SerializeField]
        private float jumpTimeout = 0.1f;

        [SerializeField] private bool isGrounded = true;
        [SerializeField] private float groundedRadius = 0.5f;
        [SerializeField] private float groundedOffset = 0.15f;
        [SerializeField] private LayerMask groundLayers;

        private float _targetSpeed;
        private float _verticalVelocity;
        private float _jumpCooldown;
        private Vector3 _inputVector;
        private float _currentHorizontalSpeed;
        private float _tolerance;

        #region Properties

        public CharacterController CharacterController => _characterController;
        public PlayerInput PlayerInput => _playerInput;
        public bool IsGrounded => isGrounded;
        public PlayerStateViewer playerStateViewer;

        #endregion

        [Header("Other Components")] private CharacterController _characterController;
        private PlayerInput _playerInput;
        private PlayerStateMachine _playerStateMachine;
        private Animator _animator;


        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _characterController = GetComponent<CharacterController>();
            _playerStateMachine = GetComponent<PlayerStateMachine>();
            _animator = GetComponent<Animator>();
        }

        private void LateUpdate()
        {
            CalculateVertical();
            Move();
        }

        private void Move()
        {
            _inputVector = _playerInput.InputVector;

            if (_inputVector == Vector3.zero)
            {
                _targetSpeed = 0;
            }

            // if we are not at target speed (outside of tolerance), lerp to the target speed
            _currentHorizontalSpeed =
                new Vector3(_characterController.velocity.x, 0.0f, _characterController.velocity.z).magnitude;
            _tolerance = 0.1f;

            if (_currentHorizontalSpeed < _targetSpeed - _tolerance ||
                _currentHorizontalSpeed > _targetSpeed + _tolerance)
            {
                _targetSpeed = Mathf.Lerp(_currentHorizontalSpeed, _targetSpeed, Time.deltaTime * acceleration);
                _targetSpeed = Mathf.Round(_targetSpeed * 1000f) / 1000f;
            }
            else
            {
                _targetSpeed = moveSpeed;
            }

            _characterController.Move((_inputVector.normalized * _targetSpeed * Time.deltaTime) +
                                      new Vector3(0f, _verticalVelocity, 0f) * Time.deltaTime);
        }

        private void CalculateVertical()
        {
            if (isGrounded)
            {
                if (_verticalVelocity < 0f)
                {
                    _verticalVelocity = -2f;
                }

                if (_playerInput.IsJumping && _jumpCooldown <= 0f)
                {
                    _verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
                }

                if (_jumpCooldown >= 0f)
                {
                    _jumpCooldown -= Time.deltaTime;
                }
            }
            else
            {
                _jumpCooldown = jumpTimeout;
                _playerInput.IsJumping = false;
            }

            _verticalVelocity += gravity * Time.deltaTime;

            // check if grounded
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y + groundedOffset,
                transform.position.z);
            isGrounded = Physics.CheckSphere(spherePosition, 0.5f, groundLayers, QueryTriggerInteraction.Ignore);
        }


        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (isGrounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
            Gizmos.DrawSphere(
                new Vector3(transform.position.x, transform.position.y + groundedOffset, transform.position.z),
                groundedRadius);
        }

        public void Attack()
        {
            _animator.SetTrigger(Strings.AttackAnimation);
        }

        public void ChangePlayerStateAttackToIdle()
        {
            _playerStateMachine.ChangeState(_playerStateMachine.idleState);
        }
    }
}
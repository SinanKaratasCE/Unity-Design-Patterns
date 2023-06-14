using UnityEngine;
using Utils;

namespace DesignPatterns.Observer
{
    public class PlayerBase : MonoBehaviour
    {
        private bool _groundedPlayer;
        [HideInInspector] public Vector3 _playerVelocity;
        private float _playerSpeed = 10.0f;
        private float _jumpHeight = 1.0f;
        private float _gravityValue = -9.81f;
        [HideInInspector] public CharacterController _characterController;

        #region Unity Methods

        void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        void Update()
        {
            GroundCheck();
            Move();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }

        #endregion

        #region Private Methods

        private void GroundCheck()
        {
            _groundedPlayer = _characterController.isGrounded;
            if (_groundedPlayer && _playerVelocity.y < 0)
            {
                _playerVelocity.y = 0f;
            }
        }

        private void Move()
        {
            var move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            _characterController.Move(move * Time.deltaTime * _playerSpeed);

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }

            _playerVelocity.y += _gravityValue * Time.deltaTime;
            _characterController.Move(_playerVelocity * Time.deltaTime);
        }

        private void Jump()
        {
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);
        }

        #endregion
    }
}
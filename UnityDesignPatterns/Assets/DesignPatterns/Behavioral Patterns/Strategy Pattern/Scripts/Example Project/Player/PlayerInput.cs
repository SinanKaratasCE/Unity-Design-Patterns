using UnityEngine;

namespace DesignPatterns.Strategy
{
    public enum WandType
    {
        Ice = 0,
        Fire = 1,
        Poison = 2
    }

    public class PlayerInput : MonoBehaviour
    {
        [Header("Controls")] [SerializeField] private KeyCode forward = KeyCode.W;
        [SerializeField] private KeyCode back = KeyCode.S;
        [SerializeField] private KeyCode left = KeyCode.A;
        [SerializeField] private KeyCode right = KeyCode.D;
        [SerializeField] private KeyCode jump = KeyCode.Space;
        [SerializeField] private KeyCode attack = KeyCode.Mouse0;
        [SerializeField] private KeyCode iceWand = KeyCode.Alpha1;
        [SerializeField] private KeyCode fireWand = KeyCode.Alpha2;
        [SerializeField] private KeyCode poisonWand = KeyCode.Alpha3;

        public Vector3 InputVector { get; private set; }
        public bool IsAttacking { get; private set; }
        public bool IsJumping { get; private set; }
        public WandType currentWand;


        private float _xInput;
        private float _zInput;
        private float _yInput;


        private void HandleInput()
        {
            _xInput = 0;
            _yInput = 0;
            _zInput = 0;

            if (Input.GetKey(forward))
            {
                _zInput++;
            }

            if (Input.GetKey(back))
            {
                _zInput--;
            }

            if (Input.GetKey(left))
            {
                _xInput--;
            }

            if (Input.GetKey(right))
            {
                _xInput++;
            }


            if (Input.GetKeyDown(iceWand))
            {
                currentWand = WandType.Ice;
            }
            else if (Input.GetKeyDown(fireWand))
            {
                currentWand = WandType.Fire;
            }
            else if (Input.GetKeyDown(poisonWand))
            {
                currentWand = WandType.Poison;
            }

            InputVector = new Vector3(_xInput, _yInput, _zInput);
            IsAttacking = Input.GetKeyDown(attack);
            IsJumping = Input.GetKeyDown(jump);
        }


        private void Update()
        {
            HandleInput();
        }
    }
}
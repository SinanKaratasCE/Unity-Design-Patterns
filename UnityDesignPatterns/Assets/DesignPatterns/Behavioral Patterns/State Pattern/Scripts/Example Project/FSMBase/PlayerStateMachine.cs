using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace DesignPatterns.State
{
    public class PlayerStateMachine : StateMachineBase
    {
        private PlayerBaseState _currentState;
        [HideInInspector] public PlayerController playerController;

        [HideInInspector] public IdleState idleState;
        public RunState runState;
        public JumpState jumpState;
        public AttackState attackState;

        private void StatesInitializer()
        {
            attackState = new AttackState(this, StateNames.AttackState);
            idleState = new IdleState(this, StateNames.IdleState);
            jumpState = new JumpState(this, StateNames.JumpState);
            runState = new RunState(this, StateNames.RunState);

            _currentState = idleState;
        }

        private void Awake()
        {
            playerController = GetComponent<PlayerController>();
        }

        private void Start()
        {
            StatesInitializer();
        }

        private void Update()
        {
            Debug.Log($"{_currentState}");
            _currentState.Tick(Time.deltaTime);
        }

        public override void ChangeState(BaseState state)
        {
            _currentState.Exit();
            _currentState = (PlayerBaseState)state;
            _currentState.Enter();
        }
    }
}
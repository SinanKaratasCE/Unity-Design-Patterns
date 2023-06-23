using UnityEngine;

namespace DesignPatterns.State
{
    public class JumpState : PlayerBaseState
    {
        public JumpState(PlayerStateMachine stateMachine, StateNames stateName) : base(stateMachine, stateName)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Tick(float deltaTime)
        {
            if (playerController.IsGrounded)
            {
                playerStateMachine.ChangeState(playerStateMachine.idleState);
            }
            
            if (playerInput.IsAttacking)
            {
                playerStateMachine.ChangeState(playerStateMachine.attackState);
            }
        }

        public override void Exit()
        {
        }
    }
}
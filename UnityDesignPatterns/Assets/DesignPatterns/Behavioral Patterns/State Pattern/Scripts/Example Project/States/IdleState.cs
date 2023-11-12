using UnityEngine;

namespace DesignPatterns.State
{
    public class IdleState : PlayerBaseState
    {
        public IdleState(PlayerStateMachine stateMachine, StateNames stateName) : base(stateMachine, stateName)
        {
        }

        

        public override void Tick(float deltaTime)
        {
            // if we slow to within a minimum velocity, transition to idling/standing
            if (Mathf.Abs(playerController.CharacterController.velocity.x) > 0.1f ||
                Mathf.Abs(playerController.CharacterController.velocity.z) > 0.1f)
            {
                playerStateMachine.ChangeState(playerStateMachine.runState);
            }

            if (!playerController.IsGrounded)
            {
                playerStateMachine.ChangeState(playerStateMachine.jumpState);
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
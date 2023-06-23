using UnityEngine;

namespace DesignPatterns.State
{
    public class AttackState : PlayerBaseState
    {
        public AttackState(PlayerStateMachine stateMachine, StateNames stateName) : base(stateMachine, stateName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            playerController.Attack();
        }

        public override void Tick(float deltaTime)
        {
            if (!playerController.IsGrounded)
            {
                playerStateMachine.ChangeState(playerStateMachine.jumpState);
            }
        }

        public override void Exit()
        {
            Debug.Log("Exiting Attack State");
        }
    }
}
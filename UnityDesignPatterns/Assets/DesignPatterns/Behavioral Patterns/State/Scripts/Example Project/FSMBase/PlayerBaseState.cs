using UnityEngine;
using Zenject.SpaceFighter;

namespace DesignPatterns.State
{
    public enum StateNames
    {
        AttackState,
        IdleState,
        JumpState,
        RunState
    }

    public abstract class PlayerBaseState : BaseState
    {
        public StateNames stateName;
        public PlayerStateMachine playerStateMachine = null;
        public PlayerController playerController = null;
        public PlayerInput playerInput = null;
        public PlayerStateViewer playerStateViewer = null;

        protected PlayerBaseState(PlayerStateMachine stateMachine, StateNames stateName)
        {
            playerStateMachine = stateMachine;
            playerController = stateMachine.playerController;
            playerInput = playerController.PlayerInput;
            playerStateViewer = playerController.playerStateViewer;
            this.stateName = stateName;
        }

        public override void Enter()
        {
            playerController.playerStateViewer.ChangeStateText(stateName);
        }
    }
}
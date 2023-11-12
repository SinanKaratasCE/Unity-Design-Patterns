using System;
using UnityEngine;
using TMPro;

namespace DesignPatterns.State
{
    public class PlayerStateViewer : MonoBehaviour
    {
        private PlayerStateMachine _playerStateMachine;
        private TMP_Text stateTextMesh;

        private void Awake()
        {
            _playerStateMachine = transform.parent.GetComponent<PlayerStateMachine>();
            stateTextMesh = GetComponent<TMP_Text>();
        }

        public void ChangeStateText(StateNames stateName)
        {
            switch (stateName)
            {
                case StateNames.AttackState:
                    stateTextMesh.color = Color.cyan;
                    break;

                case StateNames.IdleState:
                    stateTextMesh.color = Color.blue;
                    break;

                case StateNames.JumpState:
                    stateTextMesh.color = Color.green;
                    break;

                case StateNames.RunState:
                    stateTextMesh.color = Color.magenta;
                    break;
            }

            stateTextMesh.text = $"Player State :: {stateName.ToString()}";
        }
    }
}
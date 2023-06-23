using System;
using UnityEngine;

namespace DesignPatterns.State
{
    public abstract class StateMachineBase : MonoBehaviour
    {
        private BaseState currentState;

        private void Update()
        {
            // if there is a state run this logic
            currentState.Tick(Time.deltaTime);
        }

        public abstract void ChangeState(BaseState state);
    }
}
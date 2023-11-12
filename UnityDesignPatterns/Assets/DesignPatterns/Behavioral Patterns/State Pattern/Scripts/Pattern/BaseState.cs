using UnityEngine;

namespace DesignPatterns.State
{
    public abstract class BaseState
    {
        public abstract void Enter();
        public abstract void Tick(float deltaTime);
        public abstract void Exit();
    }
}
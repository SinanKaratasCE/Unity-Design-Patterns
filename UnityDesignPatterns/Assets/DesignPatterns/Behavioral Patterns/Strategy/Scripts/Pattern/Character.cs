using UnityEngine;

namespace DesignPatterns.Strategy
{
    public interface IMovementStrategy
    {
        void Move(Character character);
    }


    public class WalkMovement : IMovementStrategy
    {
        public void Move(Character character)
        {
            // Walk movement logic
        }
    }


    public class FlyMovement : IMovementStrategy
    {
        public void Move(Character character)
        {
            // Fly movement logic
        }
    }

    public class Character : MonoBehaviour
    {
        private IMovementStrategy _movementStrategy;


        public void SetMovementStrategy(IMovementStrategy strategy)
        {
            _movementStrategy = strategy;
        }


        private void Update()
        {
            _movementStrategy.Move(this);
        }
    }
}
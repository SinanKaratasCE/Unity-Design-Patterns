using UnityEngine;

namespace DesignPatterns.Command
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private LayerMask obstacleLayer;
        [SerializeField] private LayerMask finishLayer;

        private const float boardSpacing = 1f;

        public void Move(Vector3 movement)
        {
            Vector3 destination = transform.position + movement;
            transform.position = destination;
        }

        public bool IsValidMove(Vector3 movement)
        {
            return !Physics.Raycast(transform.position, movement, boardSpacing, obstacleLayer);
        }
        
        public bool IsAtGoal(Vector3 movement)
        {
            return Physics.Raycast(transform.position, movement, boardSpacing, finishLayer);
        }
    }
}
using UnityEngine;

namespace DesignPatterns.Command
{
    public class MoveCommand : ICommand
    {
        private PlayerMover _playerMover;
        private Vector3 _movement;

        // pass parameters into the constructor
        public MoveCommand(PlayerMover player, Vector3 moveVector)
        {
            this._playerMover = player;
            this._movement = moveVector;
        }

        // logic of thing to do goes here
        public void Execute()
        {
            // move by vector
            _playerMover.Move(_movement);
        }

        // undo logic goes here
        public void Undo()
        {
            // move opposite direction
            _playerMover.Move(-_movement);
        }
    }
}
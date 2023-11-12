using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Utils;

namespace DesignPatterns.Command
{
    public class InputManager : MonoBehaviour
    {
        // UI Button controls
        [Header("Button Controls")] [SerializeField]
        Button forwardButton;

        [SerializeField] Button backButton;
        [SerializeField] Button leftButton;
        [SerializeField] Button rightButton;
        [SerializeField] Button undoButton;
        [SerializeField] Button redoButton;
        private PlayerMover _playerMover;
        private GameManager _gameManager;

        [Inject]
        private void OnInstaller(PlayerMover playerMover, GameManager gameManager)
        {
            _playerMover = playerMover;
            _gameManager = gameManager;
        }

        private void Start()
        {
            // button setup
            forwardButton.onClick.AddListener(OnForwardInput);
            backButton.onClick.AddListener(OnBackInput);
            rightButton.onClick.AddListener(OnRightInput);
            leftButton.onClick.AddListener(OnLeftInput);
            undoButton.onClick.AddListener(OnUndoInput);
            redoButton.onClick.AddListener(OnRedoInput);
        }

        private void RunPlayerCommand(PlayerMover playerMover, Vector3 movement)
        {
            if (playerMover == null || _gameManager == null)
            {
                Debug.Log($"{playerMover.name} is null");
                return;
            }

            // check if there are moves left
            if (_gameManager.RemainingMoves <= 0)
            {
                Debug.Log("No more moves left");
                return;
            }

            // check if movement is unobstructed
            if (playerMover.IsValidMove(movement))
            {
                if (playerMover.IsAtGoal(movement))
                {
                    _gameManager.LevelComplete();
                    Timer.Instance.TimerWait(3f,()=>_gameManager.RestartLevel());
                }
                // issue the command and save to undo stack
                ICommand command = new MoveCommand(playerMover, movement);

                // we run the command immediately here, but you can also delay this for extra control over the timing
                CommandInvoker.ExecuteCommand(command);
                _gameManager.DecreaseMoveCount();
                
                
            }
        }

        private void OnLeftInput()
        {
            RunPlayerCommand(_playerMover, Vector3.left);
        }

        private void OnRightInput()
        {
            RunPlayerCommand(_playerMover, Vector3.right);
        }

        private void OnForwardInput()
        {
            RunPlayerCommand(_playerMover, Vector3.forward);
        }

        private void OnBackInput()
        {
            RunPlayerCommand(_playerMover, Vector3.back);
        }

        private void OnUndoInput()
        {
            if (CommandInvoker.CheckUndo())
            {
                _gameManager.IncreaseMoveCount();
            }

            CommandInvoker.UndoCommand();
        }

        private void OnRedoInput()
        {
            if (CommandInvoker.CheckRedo())
            {
                _gameManager.DecreaseMoveCount();
            }

            CommandInvoker.RedoCommand();
        }
    }
}
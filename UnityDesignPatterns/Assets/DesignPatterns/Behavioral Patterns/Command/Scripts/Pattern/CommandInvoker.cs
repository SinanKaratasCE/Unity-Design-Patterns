using UnityEngine;
using System.Collections.Generic;

namespace DesignPatterns.Command
{
    public class CommandInvoker : MonoBehaviour
    {
        // stack of command objects to undo
        private static Stack<ICommand> _undoStack = new Stack<ICommand>();

        // second stack of redoable commands
        private static Stack<ICommand> _redoStack = new Stack<ICommand>();

        // execute a command object directly and save to the undo stack
        public static void ExecuteCommand(ICommand command)
        {
            command.Execute();
            _undoStack.Push(command);

            // clear out the redo stack if we make a new move
            _redoStack.Clear();
        }

        public static void UndoCommand()
        {
            if (CheckUndo())
            {
                ICommand activeCommand = _undoStack.Pop();
                _redoStack.Push(activeCommand);
                activeCommand.Undo();
            }
        }

        public static void RedoCommand()
        {
            if (CheckRedo())
            {
                ICommand activeCommand = _redoStack.Pop();
                _undoStack.Push(activeCommand);
                activeCommand.Execute();
            }
        }

        public static bool CheckUndo()
        {
            return _undoStack.Count > 0;
        }

        public static bool CheckRedo()
        {
            return _redoStack.Count > 0;
        }
    }
}
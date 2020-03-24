using System;
using System.Collections.Generic;

namespace RG_PSI_PZ1.Core
{
    public class CommandManager : ICommandManager
    {
        public event EventHandler CommandExecuted;

        private readonly Stack<IUndoableCommand> _redoStack = new Stack<IUndoableCommand>();
        private readonly Stack<IUndoableCommand> _undoStack = new Stack<IUndoableCommand>();

        public bool CanRedo()
        {
            return _redoStack.Count > 0;
        }

        public bool CanUndo()
        {
            return _undoStack.Count > 0;
        }

        public void Execute(IUndoableCommand command)
        {
            command.Execute();
            _undoStack.Push(command);
            _redoStack.Clear();

            OnCommandExecuted();
        }

        public void Redo()
        {
            var command = _redoStack.Pop();
            command.Execute();
            _undoStack.Push(command);

            OnCommandExecuted();
        }

        public void Undo()
        {
            var command = _undoStack.Pop();
            command.UnExecute();
            _redoStack.Push(command);

            OnCommandExecuted();
        }

        protected virtual void OnCommandExecuted()
        {
            CommandExecuted?.Invoke(this, EventArgs.Empty);
        }
    }
}
using System;

namespace RG_PSI_PZ1.Core
{
    public interface ICommandManager
    {
        event EventHandler CommandExecuted;

        void Execute(IUndoableCommand command);

        void Undo();

        void Redo();

        bool CanUndo();

        bool CanRedo();
    }
}
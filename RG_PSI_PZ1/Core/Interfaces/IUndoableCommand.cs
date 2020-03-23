namespace RG_PSI_PZ1.Core
{
    public interface IUndoableCommand
    {
        void Execute();

        void UnExecute();
    }
}
namespace RG_PSI_PZ1.Core
{
    internal interface IMouseClickHandlerFactory
    {
        IMouseClickHandler GetHandler(string selectedItem);
    }
}
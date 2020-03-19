namespace RG_PSI_PZ1
{
    internal interface IMouseClickHandlerFactory
    {
        IMouseClickHandler GetHandler(string selectedItem);
    }
}
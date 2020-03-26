using System.Windows;
using System.Windows.Input;

namespace RG_PSI_PZ1.Core
{
    public interface IMouseClickHandler
    {
        void Handle(MouseButtonEventArgs e);
    }
}
using System.Windows;

namespace RG_PSI_PZ1.Core
{
    public interface IMouseClickHandler
    {
        void Handle(Point clickPoint);
    }
}
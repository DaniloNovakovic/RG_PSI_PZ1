using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RG_PSI_PZ1
{
    public class MouseClickHandlerFactory : IMouseClickHandlerFactory
    {
        private readonly IMouseClickHandler _defaultHandler;

        public MouseClickHandlerFactory()
        {
            _defaultHandler = new NullMouseClickHandler();
        }

        public IMouseClickHandler GetHandler(string selectedItem)
        {
            return _defaultHandler;
        }

        private class NullMouseClickHandler : IMouseClickHandler
        {
            public void Handle(Point clickPoint)
            {
            }
        }
    }
}
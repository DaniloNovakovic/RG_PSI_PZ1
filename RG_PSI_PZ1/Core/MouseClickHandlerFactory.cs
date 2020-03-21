using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RG_PSI_PZ1
{
    public class MouseClickHandlerFactory : IMouseClickHandlerFactory
    {
        private readonly IMouseClickHandler _defaultHandler;
        private readonly Dictionary<string, IMouseClickHandler> _handlers;

        public MouseClickHandlerFactory(Canvas canvas)
        {
            _defaultHandler = new NullMouseClickHandler();
            _handlers = new Dictionary<string, IMouseClickHandler>
            {
                ["rectangle"] = new DrawRectangleMouseClickHandler(canvas)
            };
        }

        public IMouseClickHandler GetHandler(string selectedItem)
        {
            if (!_handlers.TryGetValue(selectedItem.ToLower(), out var handler))
            {
                Debug.WriteLine($"No handlers found for {selectedItem}, returning default null handler...");
                return _defaultHandler;
            }
            return handler;
        }

        private class NullMouseClickHandler : IMouseClickHandler
        {
            public void Handle(Point clickPoint)
            {
            }
        }
    }
}
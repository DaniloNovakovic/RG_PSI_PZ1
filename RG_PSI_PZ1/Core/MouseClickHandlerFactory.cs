using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RG_PSI_PZ1.Core
{
    public class MouseClickHandlerFactory : IMouseClickHandlerFactory
    {
        private readonly IMouseClickHandler _defaultHandler;
        private readonly Dictionary<string, IMouseClickHandler> _handlers;

        public MouseClickHandlerFactory(Canvas canvas, ICommandManager commandManager)
        {
            _defaultHandler = new NullMouseClickHandler();
            _handlers = new Dictionary<string, IMouseClickHandler>
            {
                ["rectangle"] = new DrawRectangleMouseClickHandler(canvas, commandManager),
                ["ellipse"] = new DrawEllipseMouseClickHandler(canvas, commandManager),
                ["image"] = new DrawImageMouseClickHandler(canvas, commandManager)
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
            public void Handle(MouseButtonEventArgs e)
            {
            }
        }
    }
}
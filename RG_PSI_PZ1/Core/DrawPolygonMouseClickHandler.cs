using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace RG_PSI_PZ1.Core
{
    public class DrawPolygonMouseClickHandler : IMouseClickHandler
    {
        private readonly Canvas _canvas;
        private readonly ICommandManager _commandManager;

        public DrawPolygonMouseClickHandler(Canvas canvas, ICommandManager commandManager)
        {
            _canvas = canvas;
            _commandManager = commandManager;
        }

        public void Handle(MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Right)
            {
                Console.WriteLine("Right button is changed");
            }

            if (e.ChangedButton == MouseButton.Left)
            {
                Console.WriteLine("Left button is changed");
            }
        }
    }
}
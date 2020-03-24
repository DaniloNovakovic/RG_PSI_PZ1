using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace RG_PSI_PZ1.Core
{
    internal class DrawEllipseMouseClickHandler : IMouseClickHandler
    {
        private readonly Canvas _canvas;
        private readonly ICommandManager _commandManager;

        public DrawEllipseMouseClickHandler(Canvas canvas, ICommandManager commandManager)
        {
            _canvas = canvas;
            _commandManager = commandManager;
        }

        public void Handle(Point clickPoint)
        {
            var ellipse = ShowRectangleDialog(clickPoint);
            if (ellipse != null)
            {
                AttachEventHandlersToEllipse(ellipse);
                _commandManager.Execute(new DrawUIElementCommand(_canvas, ellipse, clickPoint));
            }
        }

        private void AttachEventHandlersToEllipse(Ellipse ellipse)
        {
            ellipse.MouseLeftButtonUp += (sender, e) =>
            {
                ShowRectangleDialog(e.GetPosition(_canvas), ellipseToEdit: (Ellipse)sender);
            };
        }

        private Ellipse ShowRectangleDialog(Point canvasClickPoint, Ellipse ellipseToEdit = null)
        {
            Debug.WriteLine("Opening DrawEllipseWindow dialog...");

            var absoluteClickPoint = _canvas.PointToScreen(canvasClickPoint);
            Debug.WriteLine($"AbsClickPoint: ({absoluteClickPoint.X},{absoluteClickPoint.Y})");

            var window = new DrawEllipseWindow
            {
                WindowStartupLocation = WindowStartupLocation.Manual,
                Left = absoluteClickPoint.X,
                Top = absoluteClickPoint.Y
            };

            if (ellipseToEdit != null)
            {
                window.EllipseInput = ellipseToEdit;
            }

            return window.ShowDialog() ?? false ? window.EllipseInput : null;
        }
    }
}
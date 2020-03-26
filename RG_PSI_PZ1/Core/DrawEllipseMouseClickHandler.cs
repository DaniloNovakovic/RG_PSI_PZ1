using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

        public void Handle(MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Right)
            {
                return;
            }

            Point clickPoint = e.GetPosition(_canvas);

            var ellipse = ShowEllipseDialog(clickPoint);
            if (ellipse != null)
            {
                AttachEventHandlersToEllipse(ellipse);
                Point elipseCenterPoint = MovePointToCenterOfEllipse(clickPoint, ellipse);
                _commandManager.Execute(new DrawUIElementCommand(_canvas, ellipse, elipseCenterPoint));
            }
        }

        private Point MovePointToCenterOfEllipse(Point clickPoint, Ellipse ellipse)
        {
            return new Point(x: clickPoint.X - (ellipse.Width / 2),
                             y: clickPoint.Y - (ellipse.Height / 2));
        }

        private void AttachEventHandlersToEllipse(Ellipse ellipse)
        {
            ellipse.MouseLeftButtonUp += (sender, e) =>
            {
                ShowEllipseDialog(e.GetPosition(_canvas), ellipseToEdit: (Ellipse)sender);
            };
        }

        private Ellipse ShowEllipseDialog(Point canvasClickPoint, Ellipse ellipseToEdit = null)
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
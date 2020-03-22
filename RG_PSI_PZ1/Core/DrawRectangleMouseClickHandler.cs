using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RG_PSI_PZ1
{
    internal class DrawRectangleMouseClickHandler : IMouseClickHandler
    {
        private readonly Canvas _canvas;

        public DrawRectangleMouseClickHandler(Canvas canvas)
        {
            _canvas = canvas;
        }

        public void Handle(Point clickPoint)
        {
            Debug.WriteLine("Opening DrawRectangleWindow dialog...");

            var absoluteClickPoint = _canvas.PointToScreen(clickPoint);
            Debug.WriteLine($"AbsClickPoint: ({absoluteClickPoint.X},{absoluteClickPoint.Y})");

            var window = new DrawRectangleWindow
            {
                WindowStartupLocation = WindowStartupLocation.Manual,
                Left = absoluteClickPoint.X,
                Top = absoluteClickPoint.Y
            };

            if (window.ShowDialog() ?? false)
            {
                DrawRectangleToCanvas(clickPoint, window.RectangleInput);
                AttachEventHandlersToRectangle(window.RectangleInput);
            }
        }

        private void AttachEventHandlersToRectangle(Rectangle rectangle)
        {
            rectangle.MouseLeftButtonUp += (sender, e) =>
            {
                var absoluteClickPoint = _canvas.PointToScreen(e.GetPosition(_canvas));
                var window = new DrawRectangleWindow()
                {
                    WindowStartupLocation = WindowStartupLocation.Manual,
                    Left = absoluteClickPoint.X,
                    Top = absoluteClickPoint.Y,
                    RectangleInput = rectangle
                };
                window.ShowDialog();
            };
        }

        private void DrawRectangleToCanvas(Point relativePoint, Rectangle rectangle)
        {
            Canvas.SetLeft(rectangle, relativePoint.X);
            Canvas.SetTop(rectangle, relativePoint.Y);

            _canvas.Children.Add(rectangle);
        }
    }
}
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

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

            window.ShowDialog();
        }
    }
}
using System.Windows;
using System.Windows.Controls;

namespace RG_PSI_PZ1.Core
{
    public class DrawUIElementCommand : IUndoableCommand
    {
        private readonly Canvas _canvas;
        private readonly UIElement _element;
        private readonly Point _point;

        public DrawUIElementCommand(Canvas canvas, UIElement element, Point point)
        {
            _canvas = canvas;
            _element = element;
            _point = point;
        }

        public void Execute()
        {
            Canvas.SetLeft(_element, _point.X);
            Canvas.SetTop(_element, _point.Y);

            _canvas.Children.Add(_element);
        }

        public void UnExecute()
        {
            _canvas.Children.Remove(_element);
        }
    }
}
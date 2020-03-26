using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RG_PSI_PZ1.Core
{
    public class DrawPolygonMouseClickHandler : IMouseClickHandler
    {
        private readonly Canvas _canvas;
        private readonly ICommandManager _commandManager;
        private PointCollection _points = new PointCollection();

        public DrawPolygonMouseClickHandler(Canvas canvas, ICommandManager commandManager)
        {
            _canvas = canvas;
            _commandManager = commandManager;
        }

        public void Handle(MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Right)
            {
                var point = e.GetPosition(_canvas);
                _points.Add(point);
                return;
            }

            if (e.ChangedButton == MouseButton.Left)
            {
                var fillColor = Color.FromRgb(0, 0, 255);

                var borderColor = Color.FromRgb(0, 0, 0);

                var polygon = new Polygon
                {
                    Points = _points,
                    Fill = new SolidColorBrush(fillColor),
                    Stroke = new SolidColorBrush(borderColor),
                    StrokeThickness = 2
                };

                _commandManager.Execute(new DrawUIElementCommand(_canvas, polygon, new Point(0, 0)));

                // Don't want to .Clear because it might affect Points in polygon
                _points = new PointCollection();
                return;
            }
        }
    }
}
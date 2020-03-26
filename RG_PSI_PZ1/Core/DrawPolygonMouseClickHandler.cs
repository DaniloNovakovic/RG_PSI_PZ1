using System;
using System.Diagnostics;
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
            var point = e.GetPosition(_canvas);

            if (e.ChangedButton == MouseButton.Right)
            {
                HandleRightClick(point);
            }
            else if (e.ChangedButton == MouseButton.Left && _points.Count > 1)
            {
                HandleLeftClick(point);
            }
        }

        private void HandleRightClick(Point point)
        {
            _points.Add(point);
        }

        private void HandleLeftClick(Point point)
        {
            var polygon = new Polygon { Points = _points };

            if (ShowPolygonDialog(point, polygon))
            {
                AttachEventHandlersToPolygon(polygon);
                _commandManager.Execute(new DrawUIElementCommand(_canvas, polygon, new Point(0, 0)));
            }

            // Don't want to .Clear because it might affect Points in polygon
            _points = new PointCollection();
        }

        private void AttachEventHandlersToPolygon(Polygon polygon)
        {
            polygon.MouseLeftButtonUp += (sender, e) =>
            {
                ShowPolygonDialog(e.GetPosition(_canvas), polygonToEdit: (Polygon)sender);
            };
        }

        private bool ShowPolygonDialog(Point canvasClickPoint, Polygon polygonToEdit)
        {
            Debug.WriteLine("Opening DrawPolygonWindow dialog...");

            var absoluteClickPoint = _canvas.PointToScreen(canvasClickPoint);
            Debug.WriteLine($"AbsClickPoint: ({absoluteClickPoint.X},{absoluteClickPoint.Y})");

            var window = new DrawPolygonWindow
            {
                WindowStartupLocation = WindowStartupLocation.Manual,
                Left = absoluteClickPoint.X,
                Top = absoluteClickPoint.Y
            };

            if (polygonToEdit != null)
            {
                window.PolygonInput = polygonToEdit;
            }

            return window.ShowDialog() ?? false;
        }
    }
}
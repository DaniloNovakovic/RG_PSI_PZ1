﻿using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace RG_PSI_PZ1.Core
{
    public class DrawRectangleMouseClickHandler : IMouseClickHandler
    {
        private readonly Canvas _canvas;
        private readonly ICommandManager _commandManager;

        public DrawRectangleMouseClickHandler(Canvas canvas, ICommandManager commandManager)
        {
            _canvas = canvas;
            _commandManager = commandManager;
        }

        public void Handle(Point clickPoint)
        {
            var rectangle = ShowRectangleDialog(clickPoint);
            if (rectangle != null)
            {
                AttachEventHandlersToRectangle(rectangle);
                DrawRectangleToCanvas(clickPoint, rectangle);
            }
        }

        private void AttachEventHandlersToRectangle(Rectangle rectangle)
        {
            rectangle.MouseLeftButtonUp += (sender, e) =>
            {
                ShowRectangleDialog(e.GetPosition(_canvas), rectangleToEdit: (Rectangle)sender);
            };
        }

        private void DrawRectangleToCanvas(Point relativePoint, Rectangle rectangle)
        {
            Canvas.SetLeft(rectangle, relativePoint.X);
            Canvas.SetTop(rectangle, relativePoint.Y);

            _canvas.Children.Add(rectangle);
        }

        private Rectangle ShowRectangleDialog(Point canvasClickPoint, Rectangle rectangleToEdit = null)
        {
            Debug.WriteLine("Opening DrawRectangleWindow dialog...");

            var absoluteClickPoint = _canvas.PointToScreen(canvasClickPoint);
            Debug.WriteLine($"AbsClickPoint: ({absoluteClickPoint.X},{absoluteClickPoint.Y})");

            var window = new DrawRectangleWindow
            {
                WindowStartupLocation = WindowStartupLocation.Manual,
                Left = absoluteClickPoint.X,
                Top = absoluteClickPoint.Y
            };

            if (rectangleToEdit != null)
            {
                window.RectangleInput = rectangleToEdit;
            }

            return window.ShowDialog() ?? false ? window.RectangleInput : null;
        }
    }
}
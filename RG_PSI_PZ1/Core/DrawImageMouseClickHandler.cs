using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace RG_PSI_PZ1.Core
{
    internal class DrawImageMouseClickHandler : IMouseClickHandler
    {
        private readonly Canvas _canvas;
        private readonly ICommandManager _commandManager;

        public DrawImageMouseClickHandler(Canvas canvas, ICommandManager commandManager)
        {
            _canvas = canvas;
            _commandManager = commandManager;
        }

        public void Handle(Point clickPoint)
        {
            var image = ShowImageDialog(clickPoint);
            if (image != null)
            {
                AttachEventHandlersToImage(image);
                _commandManager.Execute(new DrawUIElementCommand(_canvas, image, clickPoint));
            }
        }

        private void AttachEventHandlersToImage(Image image)
        {
            image.MouseLeftButtonUp += (sender, e) =>
            {
                ShowImageDialog(e.GetPosition(_canvas), imageToEdit: (Image)sender);
            };
        }

        private Image ShowImageDialog(Point canvasClickPoint, Image imageToEdit = null)
        {
            Debug.WriteLine("Opening DrawImageWindow dialog...");

            var absoluteClickPoint = _canvas.PointToScreen(canvasClickPoint);
            Debug.WriteLine($"AbsClickPoint: ({absoluteClickPoint.X},{absoluteClickPoint.Y})");

            var window = new DrawImageWindow
            {
                WindowStartupLocation = WindowStartupLocation.Manual,
                Left = absoluteClickPoint.X,
                Top = absoluteClickPoint.Y
            };

            if (imageToEdit != null)
            {
                window.ImageInput = imageToEdit;
            }

            return window.ShowDialog() ?? false ? window.ImageInput : null;
        }
    }
}
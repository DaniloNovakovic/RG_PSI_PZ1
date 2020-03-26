using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        public void Handle(MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Right)
            {
                return;
            }

            Point clickPoint = e.GetPosition(_canvas);

            var image = new Image { Height = 200, Width = 200, Stretch = System.Windows.Media.Stretch.Fill };
            string imageSource = "";

            ShowImageDialog(clickPoint, ref imageSource, ref image);

            if (image != null)
            {
                AttachEventHandlersToImage(image, imageSource);
                _commandManager.Execute(new DrawUIElementCommand(_canvas, image, clickPoint));
            }
        }

        private void AttachEventHandlersToImage(Image image, string imageSource)
        {
            image.MouseLeftButtonUp += (sender, e) =>
            {
                string imgSource = imageSource;
                var imgToEdit = (Image)sender;
                ShowImageDialog(e.GetPosition(_canvas), ref imgSource, ref imgToEdit);
            };
        }

        private void ShowImageDialog(Point canvasClickPoint, ref string imageSource, ref Image imageToEdit)
        {
            Debug.WriteLine("Opening DrawImageWindow dialog...");

            var absoluteClickPoint = _canvas.PointToScreen(canvasClickPoint);
            Debug.WriteLine($"AbsClickPoint: ({absoluteClickPoint.X},{absoluteClickPoint.Y})");

            var window = new DrawImageWindow(imageSource)
            {
                WindowStartupLocation = WindowStartupLocation.Manual,
                Left = absoluteClickPoint.X,
                Top = absoluteClickPoint.Y
            };

            if (imageToEdit != null)
            {
                window.ImageInput = imageToEdit;
            }

            if (window.ShowDialog() ?? false)
            {
                imageToEdit = window.ImageInput;
                imageSource = window.ImagePathLabel.Text;
            }
        }
    }
}
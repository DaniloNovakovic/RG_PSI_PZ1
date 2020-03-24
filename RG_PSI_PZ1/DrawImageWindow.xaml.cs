using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace RG_PSI_PZ1
{
    /// <summary>
    /// Interaction logic for DrawImageWindow.xaml
    /// </summary>
    public partial class DrawImageWindow : Window
    {
        private Image _image = new Image();

        public DrawImageWindow(string imageFilePath = "")
        {
            InitializeComponent();

            ImagePathLabel.Text = imageFilePath;
            SubmitButton.IsEnabled = IsPathValid(imageFilePath);
        }

        public Image ImageInput
        {
            get => _image;
            set
            {
                _image = value;
                WidthInput.Value = (int?)_image.Width;
                HeightInput.Value = (int?)_image.Height;
            }
        }

        public static bool IsPathValid(string path)
        {
            return File.Exists(path);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Cancel button clicked");

            DialogResult = false;
            Close();
        }

        private void ImagePathInputButton_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog
            {
                Filter = "Images |*.jpg;*.png;*.bmp;*.ico",
                Title = "Choose Image",
                CheckFileExists = true,
                CheckPathExists = true
            };

            if (fileDialog.ShowDialog(owner: this) == true)
            {
                ImagePathLabel.Text = fileDialog.FileName;
                SubmitButton.IsEnabled = true;
            }
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Submit button clicked");
            Debug.WriteLine($"Width: {WidthInput.Text}, Height: {HeightInput.Text}");
            Debug.WriteLine($"Image path: {ImagePathLabel.Text}");

            ImageInput.Width = WidthInput?.Value ?? 200;
            ImageInput.Height = HeightInput?.Value ?? 200;

            var uriSource = new Uri(ImagePathLabel.Text);
            ImageInput.Source = new BitmapImage(uriSource);

            DialogResult = true;
            Close();
        }
    }
}
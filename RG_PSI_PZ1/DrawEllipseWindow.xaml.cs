using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RG_PSI_PZ1
{
    /// <summary>
    /// Interaction logic for DrawEllipseWindow.xaml
    /// </summary>
    public partial class DrawEllipseWindow : Window
    {
        private Ellipse _ellipse = new Ellipse();

        public Ellipse EllipseInput
        {
            get => _ellipse;
            set
            {
                _ellipse = value;
                WidthInput.Value = (int?)_ellipse.Width;
                HeightInput.Value = (int?)_ellipse.Height;
                FillColorInput.SelectedColor = ((SolidColorBrush)_ellipse.Fill).Color;
                BorderColorInput.SelectedColor = ((SolidColorBrush)_ellipse.Stroke).Color;
                BorderThicknessInput.Value = (int?)_ellipse.StrokeThickness;
            }
        }

        public DrawEllipseWindow()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Cancel button clicked");

            DialogResult = false;
            Close();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Submit button clicked");
            Debug.WriteLine($"Width: {WidthInput.Text}, Height: {HeightInput.Text}");
            Debug.WriteLine($"Fill Color: {FillColorInput.SelectedColor}");
            Debug.WriteLine($"Border Color: {BorderColorInput.SelectedColor}, Border Thickness: {BorderThicknessInput.Text}");

            EllipseInput.Width = WidthInput?.Value ?? 200;
            EllipseInput.Height = HeightInput?.Value ?? 75;

            var fillColor = FillColorInput.SelectedColor ?? Color.FromRgb(0, 0, 255);
            EllipseInput.Fill = new SolidColorBrush(fillColor);

            var borderColor = BorderColorInput.SelectedColor ?? Color.FromRgb(0, 0, 0);
            EllipseInput.Stroke = new SolidColorBrush(borderColor);

            EllipseInput.StrokeThickness = BorderThicknessInput?.Value ?? 2;

            DialogResult = true;
            Close();
        }
    }
}
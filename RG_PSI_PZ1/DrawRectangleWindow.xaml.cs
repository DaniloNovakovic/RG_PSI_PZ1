using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RG_PSI_PZ1
{
    /// <summary>
    /// Interaction logic for DrawRectangleWindow.xaml
    /// </summary>
    public partial class DrawRectangleWindow : Window
    {
        private Rectangle _rectangle = new Rectangle();

        public Rectangle RectangleInput
        {
            get => _rectangle;
            set
            {
                _rectangle = value;
                WidthInput.Value = (int?)_rectangle.Width;
                HeightInput.Value = (int?)_rectangle.Height;
                FillColorInput.SelectedColor = ((SolidColorBrush)_rectangle.Fill).Color;
                BorderColorInput.SelectedColor = ((SolidColorBrush)_rectangle.Stroke).Color;
                BorderThicknessInput.Value = (int?)_rectangle.StrokeThickness;
            }
        }

        public DrawRectangleWindow()
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

            RectangleInput.Width = WidthInput?.Value ?? 200;
            RectangleInput.Height = HeightInput?.Value ?? 75;

            var fillColor = FillColorInput.SelectedColor ?? Color.FromRgb(0, 0, 255);
            RectangleInput.Fill = new SolidColorBrush(fillColor);

            var borderColor = BorderColorInput.SelectedColor ?? Color.FromRgb(0, 0, 0);
            RectangleInput.Stroke = new SolidColorBrush(borderColor);

            RectangleInput.StrokeThickness = BorderThicknessInput?.Value ?? 2;

            DialogResult = true;
            Close();
        }
    }
}
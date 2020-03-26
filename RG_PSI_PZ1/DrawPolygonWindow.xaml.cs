using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RG_PSI_PZ1
{
    /// <summary>
    /// Interaction logic for DrawPolygonWindow.xaml
    /// </summary>
    public partial class DrawPolygonWindow : Window
    {
        private Polygon _polygon = new Polygon();

        public Polygon PolygonInput
        {
            get => _polygon;
            set
            {
                _polygon = value;
                FillColorInput.SelectedColor = ((SolidColorBrush)_polygon.Fill)?.Color ?? Color.FromRgb(0, 0, 255);
                BorderColorInput.SelectedColor = ((SolidColorBrush)_polygon.Stroke)?.Color ?? Color.FromRgb(0, 0, 0);
                BorderThicknessInput.Value = (int?)_polygon.StrokeThickness;
            }
        }

        public DrawPolygonWindow()
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
            Debug.WriteLine($"Fill Color: {FillColorInput.SelectedColor}");
            Debug.WriteLine($"Border Color: {BorderColorInput.SelectedColor}, Border Thickness: {BorderThicknessInput.Text}");

            var fillColor = FillColorInput.SelectedColor ?? Color.FromRgb(0, 0, 255);
            PolygonInput.Fill = new SolidColorBrush(fillColor);

            var borderColor = BorderColorInput.SelectedColor ?? Color.FromRgb(0, 0, 0);
            PolygonInput.Stroke = new SolidColorBrush(borderColor);

            PolygonInput.StrokeThickness = BorderThicknessInput?.Value ?? 2;

            DialogResult = true;
            Close();
        }
    }
}
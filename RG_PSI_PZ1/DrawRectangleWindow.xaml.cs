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
    /// Interaction logic for DrawRectangleWindow.xaml
    /// </summary>
    public partial class DrawRectangleWindow : Window
    {
        public DrawRectangleWindow()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Cancel button clicked");
            Close();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Submit button clicked");
            Debug.WriteLine($"Width: {WidthInput.Text}, Height: {HeightInput.Text}");
            Debug.WriteLine($"Fill Color: {FillColorInput.SelectedColor}");
            Debug.WriteLine($"Border Color: {BorderColorInput.SelectedColor}, Border Thickness: {BorderThicknessInput.Text}");
            Close();
        }
    }
}
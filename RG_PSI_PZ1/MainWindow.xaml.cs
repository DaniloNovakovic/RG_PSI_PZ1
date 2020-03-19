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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RG_PSI_PZ1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Canvas MyCanvas { get => PaintingCanvas; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var clickedPosition = e.GetPosition(MyCanvas);
            Debug.WriteLine($"Canvas Clicked: ({clickedPosition.X}, {clickedPosition.Y})");
        }
    }
}
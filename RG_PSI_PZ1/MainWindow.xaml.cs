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
        private readonly IMouseClickHandlerFactory _mouseClickHandlerFactory;

        public MainWindow()
        {
            InitializeComponent();

            _mouseClickHandlerFactory = new MouseClickHandlerFactory(MyCanvas);
            UpdateMouseClickHandler();
        }

        public IMouseClickHandler MouseClickHandler { get; private set; }
        public Canvas MyCanvas { get => PaintingCanvas; }

        private void OnCanvasMouseClick(object sender, MouseButtonEventArgs e)
        {
            var clickedPosition = e.GetPosition(MyCanvas);

            Debug.WriteLine($"Canvas Clicked: ({clickedPosition.X}, {clickedPosition.Y})");
            MouseClickHandler?.Handle(clickedPosition);
        }

        private void OnShapeSelectionChange(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = e.AddedItems.Cast<FrameworkElement>().FirstOrDefault();
            UpdateMouseClickHandler(selectedItem?.Name ?? "");
        }

        private void UpdateMouseClickHandler(string selectedItem = "")
        {
            Debug.WriteLine($"Selected Item: {selectedItem}");
            MouseClickHandler = _mouseClickHandlerFactory.GetHandler(selectedItem);
        }
    }
}
using RG_PSI_PZ1.Core;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RG_PSI_PZ1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IMouseClickHandlerFactory _mouseClickHandlerFactory;
        private readonly ICommandManager _commandManager;

        public MainWindow()
        {
            InitializeComponent();

            _commandManager = new Core.CommandManager();
            _mouseClickHandlerFactory = new MouseClickHandlerFactory(MyCanvas, _commandManager);

            _commandManager.CommandExecuted += OnUndoableCommandExecuted;
            UpdateMouseClickHandler();
        }

        private void OnUndoableCommandExecuted(object sender, System.EventArgs e)
        {
            UndoButton.IsEnabled = _commandManager.CanUndo();
            RedoButton.IsEnabled = _commandManager.CanRedo();
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

        private void OnClear(object sender, RoutedEventArgs e)
        {
            if (MyCanvas.Children.Count > 0)
            {
                MyCanvas.Children.Clear();
            }
        }

        private void OnUndo(object sender, RoutedEventArgs e)
        {
            _commandManager.Undo();
        }

        private void OnRedo(object sender, RoutedEventArgs e)
        {
            _commandManager.Redo();
        }
    }
}
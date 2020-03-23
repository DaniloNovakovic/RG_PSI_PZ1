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
        private readonly ICommandManager _commandManager;
        private readonly IMouseClickHandlerFactory _mouseClickHandlerFactory;

        public MainWindow()
        {
            InitializeComponent();

            _commandManager = new Core.CommandManager();
            _mouseClickHandlerFactory = new MouseClickHandlerFactory(MyCanvas, _commandManager);

            _commandManager.CommandExecuted += OnUndoableCommandExecuted;
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

        private void OnClear(object sender, RoutedEventArgs e)
        {
            if (MyCanvas.Children.Count > 0)
            {
                _commandManager.Execute(new ClearCanvasCommand(MyCanvas));
            }
        }

        private void OnRedo(object sender, RoutedEventArgs e)
        {
            _commandManager.Redo();
        }

        private void OnShapeSelectionChange(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = e.AddedItems.Cast<FrameworkElement>().FirstOrDefault();
            UpdateMouseClickHandler(selectedItem?.Name ?? "");
        }

        private void OnUndo(object sender, RoutedEventArgs e)
        {
            _commandManager.Undo();
        }

        private void OnUndoableCommandExecuted(object sender, System.EventArgs e)
        {
            UndoButton.IsEnabled = _commandManager.CanUndo();
            RedoButton.IsEnabled = _commandManager.CanRedo();
        }

        private void UpdateMouseClickHandler(string selectedItem = "")
        {
            Debug.WriteLine($"Selected Item: {selectedItem}");
            MouseClickHandler = _mouseClickHandlerFactory.GetHandler(selectedItem);
        }
    }
}
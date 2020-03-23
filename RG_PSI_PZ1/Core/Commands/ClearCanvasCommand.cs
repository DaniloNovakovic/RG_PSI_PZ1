using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RG_PSI_PZ1.Core
{
    public class ClearCanvasCommand : IUndoableCommand
    {
        private readonly Canvas _canvas;
        private readonly List<UIElement> _clearedElements;

        public ClearCanvasCommand(Canvas canvas)
        {
            _canvas = canvas;
            _clearedElements = new List<UIElement>();

            foreach (UIElement child in _canvas.Children)
            {
                _clearedElements.Add(child);
            }
        }

        public void Execute()
        {
            _canvas.Children.Clear();
        }

        public void UnExecute()
        {
            _clearedElements.ForEach(element => _canvas.Children.Add(element));
        }
    }
}
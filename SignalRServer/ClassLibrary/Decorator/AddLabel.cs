using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ClassLibrary.Decorator
{
    public class AddLabel : Decorator
    {
        public AddLabel(IDecorator decoratedShape) : base(decoratedShape)
        {
        }

        public override Grid Draw()
        {
            Grid grid = decoratedShape.Draw();
            TextBlock textBlock = new TextBlock();
            textBlock.Text = grid.Name;
            textBlock.VerticalAlignment = VerticalAlignment.Bottom;
            textBlock.Margin = new Thickness(
                top: 50,
                left: 0,
                right: 0,
                bottom: 0
            );
            textBlock.FontWeight = FontWeights.Bold;
            textBlock.Foreground = new SolidColorBrush(Colors.Red);
            grid.Children.Add(textBlock);
            return grid;
        } 
    }
}

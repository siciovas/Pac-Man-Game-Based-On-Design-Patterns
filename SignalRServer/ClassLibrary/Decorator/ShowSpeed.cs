using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace ClassLibrary.Decorator
{
    public class ShowSpeed : Decorator
    {
        private string Speed;
        public ShowSpeed(IDecorator decoratedShape, string speed) : base(decoratedShape)
        {
            Speed = speed;
        }

        public override Grid Draw()
        {
            Grid grid = decoratedShape.Draw();
            TextBlock textBlock = new TextBlock();
            textBlock.Text = "Speed";
            textBlock.VerticalAlignment = VerticalAlignment.Bottom;
            textBlock.Margin = new Thickness(
                top: 60,
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

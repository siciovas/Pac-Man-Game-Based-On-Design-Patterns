using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;

namespace ClassLibrary.Decorator
{
    public class AddHealthBar : Decorator
    {
        private int Health;
        public AddHealthBar(IDecorator decoratedShape, int health) : base(decoratedShape)
        {
            Health = health;
        }

        public override Grid Draw()
        {
            Grid grid = decoratedShape.Draw();
            Rectangle rect = new Rectangle();
            var temp = Health * 30 / 100;
            rect.Width = temp > 0 ? temp : 1;
            rect.Height = 10;
            rect.Fill = new SolidColorBrush(Colors.Red);
            rect.VerticalAlignment = VerticalAlignment.Top;
            rect.Margin = new Thickness
            (
                top: 0,
                right: 0,
                left: 0,
                bottom: 50
            );
            grid.Children.Add(rect);
            return grid;
        }
        
    }
}

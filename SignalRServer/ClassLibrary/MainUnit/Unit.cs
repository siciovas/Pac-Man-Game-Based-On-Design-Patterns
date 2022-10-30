using ClassLibrary.Decorator;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ClassLibrary.MainUnit
{
    public abstract class Unit : IDecorator
    {
        public int Top { get; set; }
        public int Left { get; set; }
        public ImageBrush Appearance { get; set; }
        public string Name { get; set; }

        public override Grid Draw()
        {
            Grid grid = new Grid();
            grid.Name = Name;
            Rectangle rect = new Rectangle();
            rect.Width = 30;
            rect.Height = 30;
            if (Appearance != null)
            {
                rect.Fill = Appearance;
            }
            grid.Children.Add(rect);
            return grid;
        }

        public abstract Unit Copy();
    }
}

using ClassLibrary.Decorator;
using ClassLibrary.Visitor;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ClassLibrary.MainUnit
{
    public abstract class Unit : IDecorator, Element, INotifyPropertyChanged
    {
        public int _Top;
        public int _Left;
        public int Top
        {
            get
            {
                return _Top;
            }
            set
            {
                _Top = value;
                NotifyPropertyChanged("Top");
            }
        }
        public int Left
        {
            get
            {
                return _Left;
            }
            set
            {
                _Left = value;
                NotifyPropertyChanged("Left");
            }
        }

        public UnitType _UnitType { get; set; }
        public UnitType UnitType
        {
            get
            {
                return _UnitType;
            }
            set
            {
                _UnitType = value;
                NotifyPropertyChanged("UnitType");
            }
        }
        /*public ImageBrush Appearance { get; set; }
        public string Name { get; set; }*/


        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override Grid Draw()
        {
            Grid grid = new Grid();
            grid.Name = UnitType.Name;
            Rectangle rect = new Rectangle();
            rect.Width = 30;
            rect.Height = 30;
            if (UnitType.Appearance != null)
            {
                rect.Fill = UnitType.Appearance;
            }
            grid.Children.Add(rect);
            return grid;
        }

        public abstract Unit Copy();

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

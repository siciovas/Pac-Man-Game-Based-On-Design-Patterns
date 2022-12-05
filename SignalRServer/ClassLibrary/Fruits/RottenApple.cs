using ClassLibrary.MainUnit;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ClassLibrary.Fruits
{
    public class RottenApple : Unit
    {
        public RottenApple(int top, int left)
        {
            UnitType = new UnitType();

            Top = top;
            Left = left;
            ImageBrush rottenApple = new ImageBrush();
            rottenApple.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/rottenApple.png"));
            UnitType.Appearance = rottenApple;
            UnitType.Name = "Rotten apple";
        }
        public override RottenApple Copy()
        {
            return (RottenApple)this.MemberwiseClone();
        }
    }
}

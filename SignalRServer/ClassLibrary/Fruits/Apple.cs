using ClassLibrary.Coins;
using ClassLibrary.MainUnit;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ClassLibrary.Fruits
{
    public class Apple : Unit
    {
        public Apple(int top, int left)
        {
            Top = top;
            Left = left;
            ImageBrush apple = new ImageBrush();
            apple.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/apple.png"));
            Appearance = apple;
            Name = "Apple";
        }

        public override Apple Copy()
        {
            return (Apple)this.MemberwiseClone();
        }
    }
}

using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ClassLibrary.Fruits
{
    public class Apple
    {
        public int Top { get; set; }
        public int Left { get; set; }
        public ImageBrush Fill { get; set; }

        public Apple(int top, int left)
        {
            Top = top;
            Left = left;
            ImageBrush apple = new ImageBrush();
            apple.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/apple.png"));
            Fill = apple;
        }
    }
}

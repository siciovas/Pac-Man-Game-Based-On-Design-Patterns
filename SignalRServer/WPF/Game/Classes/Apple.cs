using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPF.Game.Classes
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

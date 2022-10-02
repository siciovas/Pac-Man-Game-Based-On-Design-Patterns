using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPF.Game.AbstractFactory.Interfaces;

namespace WPF.Game.AbstractFactory.Classes.WeakMob
{
    public class WeakGhost : IGhost
    {
        public int Speed { get; set; }
        public int Top { get; set; }
        public int Left { get; set; }
        public ImageBrush Fill { get; set; }

        public WeakGhost(int top, int left)
        {
            Speed = 8;
            Top = top;
            Left = left;
            ImageBrush ghost = new ImageBrush();
            ghost.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/red.jpg"));
            Fill = ghost;
        }
    }
}

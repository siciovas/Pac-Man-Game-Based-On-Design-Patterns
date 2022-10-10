using ClassLibrary.Mobs.Interfaces;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ClassLibrary.Mobs.StrongMob
{
    public class StrongZombie : IZombie
    {
        public int Speed { get; set; }
        public int Top { get; set; }
        public int Left { get; set; }
        public ImageBrush Fill { get; set; }

        public StrongZombie(int top, int left)
        {
            Speed = 8;
            Top = top;
            Left = left;
            ImageBrush ghost = new ImageBrush();
            ghost.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/weakZombie.png"));
            Fill = ghost;
        }
    }
}

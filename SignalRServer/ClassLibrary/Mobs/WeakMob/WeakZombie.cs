using ClassLibrary.Mobs.Interfaces;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ClassLibrary.Mobs.WeakMob
{
    public class WeakZombie : Mob, IZombie
    {
        public int Top { get; set; }
        public int Left { get; set; }
        public ImageBrush Fill { get; set; }

        public WeakZombie(int top, int left, string name) : base(name, top, left)
        {
            Top = top;
            Left = left;
            ImageBrush ghost = new ImageBrush();
            ghost.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/WeakZombie.png"));
            Fill = ghost;
        }
    }
}

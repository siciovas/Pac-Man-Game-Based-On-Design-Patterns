using ClassLibrary.Mobs.Interfaces;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ClassLibrary.Mobs.WeakMob
{
    public class WeakGhost : Mob, IGhost
    {
        public ImageBrush Fill { get; set; }
        public int Top { get => GetTop(); }
        public int Left { get => GetLeft(); }

        public WeakGhost(int top, int left, string name) : base(name, top, left)
        {
            ImageBrush ghost = new ImageBrush();
            ghost.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/red.jpg"));
            Fill = ghost;
        }
    }
}

using ClassLibrary.Mobs.Interfaces;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ClassLibrary.Mobs.StrongMob
{
    public class StrongGhost : Mob, IGhost
    {
        public ImageBrush Fill { get; set; }

        public StrongGhost(int top, int left, string name) : base(name, top, left)
        {
            ImageBrush ghost = new ImageBrush();
            ghost.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/strongGhost.png"));
            Fill = ghost;
        }
    }
}

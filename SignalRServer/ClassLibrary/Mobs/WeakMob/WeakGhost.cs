using ClassLibrary.Fruits;
using ClassLibrary.MainUnit;
using ClassLibrary.Mobs.Interfaces;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ClassLibrary.Mobs.WeakMob
{
    public class WeakGhost : Mob
    {
        public WeakGhost(int top, int left)
        {
            Top = top;
            Left = left;
            ImageBrush ghost = new ImageBrush();
            ghost.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/red.jpg"));
            Appearance = ghost;
            Name = "Weak ghost";
        }

        public override WeakGhost Copy()
        {
            return (WeakGhost)this.MemberwiseClone();
        }
    }
}

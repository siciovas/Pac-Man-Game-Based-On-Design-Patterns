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
            UnitType = new UnitType();

            Top = top;
            Left = left;
            GoLeft = true;
            ImageBrush ghost = new ImageBrush();
            ghost.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/red.jpg"));
            UnitType.Appearance = ghost;
            UnitType.Name = "Weak ghost";
        }

        public override WeakGhost Copy()
        {
            return (WeakGhost)this.MemberwiseClone();
        }
    }
}

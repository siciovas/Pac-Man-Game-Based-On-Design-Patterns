using ClassLibrary.Fruits;
using ClassLibrary.MainUnit;
using ClassLibrary.Mobs.Interfaces;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ClassLibrary.Mobs.WeakMob
{
    public class WeakDemogorgon : Mob
    {
        public WeakDemogorgon(int top, int left)
        {
            UnitType = new UnitType();

            Top = top;
            Left = left;
            GoLeft = true;
            ImageBrush demo = new ImageBrush();
            demo.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/WeakDemo.png"));
            UnitType.Appearance = demo;
            UnitType.Name = "Weak demogorgon";
        }

        public override WeakDemogorgon Copy()
        {
            return (WeakDemogorgon)this.MemberwiseClone();
        }
    }
}

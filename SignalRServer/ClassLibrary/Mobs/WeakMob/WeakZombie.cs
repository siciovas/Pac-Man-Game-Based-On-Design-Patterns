using ClassLibrary.Fruits;
using ClassLibrary.MainUnit;
using ClassLibrary.Mobs.Interfaces;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ClassLibrary.Mobs.WeakMob
{
    public class WeakZombie : Mob
    {
        public WeakZombie(int top, int left)
        {
            Top = top;
            Left = left;
            ImageBrush zombie = new ImageBrush();
            zombie.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/WeakZombie.png"));
            Appearance = zombie;
            Name = "Weak zombie";
        }

        public override WeakZombie Copy()
        {
            return (WeakZombie)this.MemberwiseClone();
        }
    }
}

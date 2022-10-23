using ClassLibrary.Fruits;
using ClassLibrary.MainUnit;
using ClassLibrary.Mobs.Interfaces;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ClassLibrary.Mobs.StrongMob
{
    public class StrongZombie : Mob
    {
        public StrongZombie(int top, int left)
        {
            Top = top;
            Left = left;
            ImageBrush zombie = new ImageBrush();
            zombie.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/StrongZombie.png"));
            Appearance = zombie;
            Name = "Strong zombie";
        }

        public override StrongZombie Copy()
        {
            return (StrongZombie)this.MemberwiseClone();
        }
    }
}

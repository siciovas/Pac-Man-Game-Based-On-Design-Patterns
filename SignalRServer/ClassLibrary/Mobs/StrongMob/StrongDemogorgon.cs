using ClassLibrary.Fruits;
using ClassLibrary.MainUnit;
using ClassLibrary.Mobs.Interfaces;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ClassLibrary.Mobs.StrongMob
{
    public class StrongDemogorgon : Mob
    {
        public StrongDemogorgon(int top, int left)
        {
            Top = top;
            Left = left;
            GoLeft = true;
            ImageBrush demo = new ImageBrush();
            demo.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/StrongDemo.png"));
            Appearance = demo;
            Name = "Strong demogorgon";
        }

        public override StrongDemogorgon Copy()
        {
            return (StrongDemogorgon)this.MemberwiseClone();
        }
    }
}
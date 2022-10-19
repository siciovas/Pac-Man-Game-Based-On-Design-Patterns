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
            Top = top;
            Left = left;
            ImageBrush demo = new ImageBrush();
            demo.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/WeakDemo.png"));
            Appearance = demo;
            Name = "Weak demogorgon";
        }
    }
}

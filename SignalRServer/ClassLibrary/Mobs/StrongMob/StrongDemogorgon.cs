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
            ImageBrush demo = new ImageBrush();
            demo.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/StrongDemo.png"));
            Appearance = demo;
            Name = "Strong demogorgon";
        }
    }
}
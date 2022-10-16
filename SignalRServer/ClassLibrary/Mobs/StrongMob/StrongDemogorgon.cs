using ClassLibrary.Mobs.Interfaces;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ClassLibrary.Mobs.StrongMob
{
    public class StrongDemogorgon : Mob
    {
        public int Top { get; set; }
        public int Left { get; set; }
        public ImageBrush Fill { get; set; }

        public StrongDemogorgon(int top, int left, string name) : base(name)
        {
            Top = top;
            Left = left;
            ImageBrush demo = new ImageBrush();
            demo.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/StrongDemo.png"));
            Fill = demo;
        }
    }
}
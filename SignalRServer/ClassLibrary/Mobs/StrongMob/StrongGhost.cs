using ClassLibrary.Fruits;
using ClassLibrary.MainUnit;
using ClassLibrary.Mobs.Interfaces;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ClassLibrary.Mobs.StrongMob
{
    public class StrongGhost : Mob
    {
        public StrongGhost(int top, int left)
        {
            Top = top;
            Left = left;
            GoLeft = true;
            ImageBrush ghost = new ImageBrush();
            ghost.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/strongGhost.png"));
            Appearance = ghost;
            Name = "Strong ghost";
        }

        public override Unit Copy()
        {
            return (StrongGhost)this.MemberwiseClone();
        }
    }
}

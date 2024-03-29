﻿using ClassLibrary.Fruits;
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
            UnitType = new UnitType();

            Top = top;
            Left = left;
            GoLeft = true;
            ImageBrush zombie = new ImageBrush();
            zombie.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/StrongZombie.png"));
            UnitType.Appearance = zombie;
            UnitType.Name = "Strong zombie";
        }

        public override StrongZombie Copy()
        {
            return (StrongZombie)this.MemberwiseClone();
        }
    }
}

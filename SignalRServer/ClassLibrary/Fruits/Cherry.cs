﻿using ClassLibrary.Coins;
using ClassLibrary.MainUnit;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ClassLibrary.Fruits
{
    public class Cherry : Unit
    {
        public Cherry(int top, int left)
        {
            UnitType = new UnitType();

            Top = top;
            Left = left;
            ImageBrush cherry = new ImageBrush();
            cherry.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/cherry.png"));
            UnitType.Appearance = cherry;
            UnitType.Name = "Cherry";
        }

        public override Cherry Copy()
        {
            return (Cherry)this.MemberwiseClone();
        }
    }
}

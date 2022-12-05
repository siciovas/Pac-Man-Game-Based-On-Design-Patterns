using ClassLibrary.Coins.Interfaces;
using ClassLibrary.MainUnit;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ClassLibrary.Coins
{
    public class GoldCoin : Coin
    {
        public GoldCoin() : base()
        {
            UnitType = new UnitType();

            Value = 5;
            Top = 50;
            Left = 50;
            ImageBrush goldCoin = new ImageBrush();
            goldCoin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/goldCoin.png"));
            UnitType.Appearance = goldCoin;
            UnitType.Name = "Gold coin";
        }

        public override Coin Copy() //shallow copy
        {
            return (GoldCoin)this.MemberwiseClone();
        }
    }
}

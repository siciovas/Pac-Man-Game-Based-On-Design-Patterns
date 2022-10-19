using ClassLibrary.Coins.Interfaces;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ClassLibrary.Coins
{
    public class SilverCoin : Coin
    {
        public SilverCoin() : base()
        {
            Value = 3;
            Top = 50;
            Left = 50;
            ImageBrush silverCoin = new ImageBrush();
            silverCoin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/silverCoin.png"));
            Appearance = silverCoin;
            Name = "Silver coin";
        }

        public override Coin Copy()
        {
            return (SilverCoin)this.MemberwiseClone();
        }
    }
}

using ClassLibrary.Coins.Interfaces;
using ClassLibrary.MainUnit;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ClassLibrary.Coins
{
    public class BronzeCoin : Coin
    {
        public BronzeCoin() : base()
        {
            Value = 2;
            Top = 50;
            Left = 50;
            ImageBrush bronzeCoin = new ImageBrush();
            bronzeCoin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/bronzeCoin.png"));
            Appearance = bronzeCoin;
            Name = "Bronze coin";
        }

  
        public override Coin Copy()
        {
            return (BronzeCoin)this.MemberwiseClone();
        }
    }
}

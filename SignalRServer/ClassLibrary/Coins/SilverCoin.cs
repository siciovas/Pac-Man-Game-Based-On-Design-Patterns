using ClassLibrary.Coins.Interfaces;
using ClassLibrary.MainUnit;
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
            var factory = new UnitFactory();
            UnitType = factory.GetFlyweight("silverCoin");
        }

        public override Coin Copy()
        {
            return (SilverCoin)this.MemberwiseClone();
        }
    }
}

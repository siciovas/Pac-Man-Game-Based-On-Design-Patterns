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
            var unitFactory = new UnitFactory();
            UnitType = unitFactory.GetFlyweight("goldCoin");
        }

        public override Coin Copy() //shallow copy
        {
            return (GoldCoin)this.MemberwiseClone();
        }
    }
}

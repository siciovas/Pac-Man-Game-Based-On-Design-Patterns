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
            UnitType = new UnitType();

            Value = 2;
            Top = 50;
            Left = 50;
            var unitFactory = new UnitFactory();
            UnitType = unitFactory.GetFlyweight("bronzeCoin");
        }

  
        public override Coin Copy()
        {
            return (BronzeCoin)this.MemberwiseClone();
        }
    }
}

using ClassLibrary.Coins.Interfaces;
using System;

namespace ClassLibrary.Coins
{
    public class GoldCoin : ICoin
    {
        public GoldCoin()
        {
            Value = 5;
            Color = "gold";
            Left = 50;
            Top = 50;
        }
        public int Value { get; set; }
        public double Left { get; set; }
        public double Top { get; set; }
        public string Color { get; set; }
        public ICoin Copy() //shallow copy
        {
            return (GoldCoin)this.MemberwiseClone();
        }
    }
}

using ClassLibrary.Coins.Interfaces;

namespace ClassLibrary.Coins
{
    public class SilverCoin : ICoin
    {
        public SilverCoin()
        {
            Value = 3;
            Color = "silver";
            Left = 50;
            Top = 50;
        }
        public int Value { get; set; }
        public double Left { get; set; }
        public double Top { get; set; }
        public string Color { get; set; }

        public ICoin Copy()
        {
            return (SilverCoin)this.MemberwiseClone();
        }
    }
}

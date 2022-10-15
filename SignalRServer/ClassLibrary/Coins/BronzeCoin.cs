using ClassLibrary.Coins.Interfaces;

namespace ClassLibrary.Coins
{
    public class BronzeCoin : ICoin
    {
        public BronzeCoin()
        {
            Value = 2;
            Color = "RosyBrown";
            Left = 50;
            Top = 50;  
        }
        public double Top { get; set; }
        public double Left { get; set; }
        public int Value { get; set; }
        public string Color { get; set; }
        public ICoin Copy()
        {
            return (BronzeCoin)this.MemberwiseClone();
        }
    }
}

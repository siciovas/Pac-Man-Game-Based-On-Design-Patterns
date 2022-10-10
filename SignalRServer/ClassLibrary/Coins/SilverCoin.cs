using ClassLibrary.Coins.Interfaces;

namespace ClassLibrary.Coins
{
    public class SilverCoin : ICoin
    {
        public SilverCoin(int left, int top)
        {
            Value = 3;
            Color = "silver";
            Left = left;
            Top = top;
        }
        public int Value { get; set; }
        public double Left { get; set; }
        public double Top { get; set; }
        public string Color { get; set; }
    }
}

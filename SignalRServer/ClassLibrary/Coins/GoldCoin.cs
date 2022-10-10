using ClassLibrary.Coins.Interfaces;

namespace ClassLibrary.Coins
{
    public class GoldCoin : ICoin
    {
        private double left;
        private double top;

        public GoldCoin(int left, int top)
        {
            Value = 5;
            Color = "gold";
            Left = left;
            Top = top;
        }
        public int Value { get; set; }
        public double Left { get => left; set => left = value; }
        public double Top { get => top; set => top = value; }
        public string Color { get; set; }
    }
}

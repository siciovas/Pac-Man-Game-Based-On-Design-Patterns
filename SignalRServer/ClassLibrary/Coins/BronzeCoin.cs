using ClassLibrary.Coins.Interfaces;

namespace ClassLibrary.Coins
{
    public class BronzeCoin : ICoin
    {
        public BronzeCoin(int left, int top)
        {
            Value = 2;
            Color = "RosyBrown";
            Left = left;
            Top = top;  
        }
        public int Value { get; set; }
        public double Left { get; set; }
        public double Top { get; set; }    
        public string Color { get; set; }
    }
}

using ClassLibrary.Coins.Interfaces;

namespace ClassLibrary.Coins.Factories
{
    public class SilverCoinCreator : CoinFactory
    {
        private ICoin CoinToCopy = new SilverCoin();
        public override ICoin GetCoin(int left, int top)
        {
            var copy = CoinToCopy.Copy();
            copy.Left = left;
            copy.Top = top;
            return copy;
        }
    }
}

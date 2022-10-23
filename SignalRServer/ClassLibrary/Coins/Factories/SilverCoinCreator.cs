using ClassLibrary.Coins.Interfaces;

namespace ClassLibrary.Coins.Factories
{
    public class SilverCoinCreator : CoinFactory
    {
        private Coin CoinToCopy = new SilverCoin();
        public override Coin GetCoin(int left, int top)
        {
            var copy = CoinToCopy.Copy();
            copy.Left = left;
            copy.Top = top;
            return (Coin)copy;
        }
    }
}

using ClassLibrary.Coins.Interfaces;

namespace ClassLibrary.Coins.Factories
{
    public class GoldCoinCreator : CoinFactory
    {
        private Coin CoinToCopy;
        public GoldCoinCreator()
        {
            CoinToCopy = new GoldCoin();
        }

        public override Coin GetCoin(int left, int top)
        {
            var copy = CoinToCopy.Copy();
            copy.Left = left;
            copy.Top = top;
            return copy;
        }
    }
}

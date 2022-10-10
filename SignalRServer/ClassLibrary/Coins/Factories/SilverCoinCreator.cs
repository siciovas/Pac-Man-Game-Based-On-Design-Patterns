using ClassLibrary.Coins.Interfaces;

namespace ClassLibrary.Coins.Factories
{
    public class SilverCoinCreator : CoinFactory
    {
        public override ICoin GetCoin(int left, int top)
        {
            return new SilverCoin(left, top);
        }
    }
}

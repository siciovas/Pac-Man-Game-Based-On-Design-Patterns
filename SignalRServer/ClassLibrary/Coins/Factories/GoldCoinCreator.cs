using ClassLibrary.Coins.Interfaces;

namespace ClassLibrary.Coins.Factories
{
    public class GoldCoinCreator : CoinFactory
    {
        public override ICoin GetCoin(int left, int top)
        {
            return new GoldCoin(left, top);
        }
    }
}

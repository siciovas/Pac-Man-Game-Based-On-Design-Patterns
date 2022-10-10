using ClassLibrary.Coins.Interfaces;

namespace ClassLibrary.Coins.Factories
{
    public class BronzeCoinCreator : CoinFactory
    {
        public override ICoin GetCoin(int left, int top)
        {
            return new BronzeCoin(left, top);
        }
    }
}

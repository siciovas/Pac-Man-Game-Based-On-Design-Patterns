using ClassLibrary.Coins.Interfaces;

namespace ClassLibrary.Coins.Factories
{
    public abstract class CoinFactory
    {
        abstract public Coin GetCoin(int left, int top);
    }
}

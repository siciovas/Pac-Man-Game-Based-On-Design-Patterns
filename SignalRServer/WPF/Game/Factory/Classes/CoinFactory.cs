using WPF.Game.Factory.Interfaces;

namespace WPF.Game.Factory.Classes
{
    public class CoinFactory
    {
        public ICoin GetCoin(int level)
        {
            switch (level)
            {
                case 1:
                    return new BronzeCoin();
                case 2:
                    return new SilverCoin();
                default:
                    return new GoldCoin();
            }
        }
    }
}

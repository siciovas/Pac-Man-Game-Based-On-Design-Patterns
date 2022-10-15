using ClassLibrary.Coins.Interfaces;
using System.Security.RightsManagement;

namespace ClassLibrary.Coins.Factories
{
    public class GoldCoinCreator : CoinFactory
    {
        private ICoin CoinToCopy;
        public GoldCoinCreator()
        {
            CoinToCopy = new GoldCoin();
        }

        public override ICoin GetCoin(int left, int top)
        {
            var copy = CoinToCopy.Copy();
            copy.Left = left;
            copy.Top = top;
            return copy;
        }
    }
}

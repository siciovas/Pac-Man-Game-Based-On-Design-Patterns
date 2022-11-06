using ClassLibrary.Coins.Factories;
using ClassLibrary.Coins.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.CoinMapping
{
    public class CoinMapProvider
    {
        private CoinFactory _coinFactory;

        public CoinMapProvider(CoinFactory coinFactory)
        {
            _coinFactory = coinFactory;
        }

        public ObservableCollection<Coin> GetCoins(int iFrom, int iTo, int jFrom, int jTo)
        {
            ObservableCollection<Coin> result = new ObservableCollection<Coin>();
            for (int i = iFrom; i < iTo; i = i + 50)
            {
                for (int j = jFrom; j < jTo; j = j + 50)
                {
                    if (i == iFrom && j == jFrom) continue;
                    var coin = _coinFactory.GetCoin(i, j);
                    result.Add(coin);
                }
            }
            return result;
        }
    }
}

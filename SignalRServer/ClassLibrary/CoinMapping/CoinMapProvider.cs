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

        public ObservableCollection<Coin> GetCoins()
        {
            ObservableCollection<Coin> result = new ObservableCollection<Coin>();
            for (int i = 10; i < 800; i = i + 50)
            {
                for (int j = 50; j < 600; j = j + 50)
                {
                    if (i == 10 && j == 50) continue;
                    var coin = _coinFactory.GetCoin(i, j);
                    result.Add(coin);
                }
            }
            return result;
        }

        public ObservableCollection<Coin> GetVariousCoins(CoinFactory _otherFactory)
        {
            ObservableCollection<Coin> result = new ObservableCollection<Coin>();
            for (int i = 10; i < 800; i = i + 50)
            {
                for (int j = 50; j < 300; j = j + 50)
                {
                    if (i == 10 && j == 50) continue;
                    var coin = _coinFactory.GetCoin(i, j);
                    result.Add(coin);
                }
            }
            for (int i = 10; i < 800; i = i + 50)
            {
                for (int j = 300; j < 600; j = j + 50)
                {
                    var coin = _otherFactory.GetCoin(i, j);
                    result.Add(coin);
                }
            }
            return result;
        }

        /*public ObservableCollection<Coin> GetFirstHalfCoins(CoinFactory _coinFactory)
        {
            ObservableCollection<Coin> result = new ObservableCollection<Coin>();
            for (int i = 10; i < 800; i = i + 50)
            {
                for (int j = 50; j < 300; j = j + 50)
                {
                    if (i == 10 && j == 50) continue;
                    var coin = _coinFactory.GetCoin(i, j);
                    result.Add(coin);
                }
            }
            return result;
        }

        public ObservableCollection<Coin> GetSecondHalfCoins(CoinFactory _coinFactory, ObservableCollection<Coin> Coins)
        {
            //ObservableCollection<Coin> result = new ObservableCollection<Coin>();
            for (int i = 10; i < 800; i = i + 50)
            {
                for (int j = 300; j < 600; j = j + 50)
                {
                    var coin = _coinFactory.GetCoin(i, j);
                    Coins.Add(coin);
                }
            }
            return Coins;
        }*/
    }
}

using ClassLibrary.Adapter.Interfaces;
using ClassLibrary.CoinMapping;
using ClassLibrary.Coins.Factories;
using ClassLibrary.Coins.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Adapter
{
    public class CoinMapProviderAdapter : ICoinMapProvider
    {
        private CoinMapProvider adaptee;

        public CoinMapProviderAdapter(CoinMapProvider adaptee)
        {
            this.adaptee = adaptee;
        }

        public ObservableCollection<Coin> GetFirstHalfCoins(CoinFactory coinFactory)
        {
            ObservableCollection<Coin> result = adaptee.GetCoins(10, 800, 50, 300, coinFactory); //perduodi ta pati kas pas tave cikle buvo nuo i iki i ir nuo j iki j

            return result;
        }

        public ObservableCollection<Coin> GetSecondHalfCoins(CoinFactory coinFactory, ObservableCollection<Coin> Coins)
        {
            ObservableCollection<Coin> result = adaptee.GetCoins(10, 800, 300, 600, coinFactory);
            foreach (var item in result)
            {
                Coins.Add(item);
            }
            return Coins;
        }
    }
}

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

        public ObservableCollection<Coin> GetCoins(CoinFactory coinFactory)
        {
            return adaptee.GetVariousCoins(coinFactory);
        }
    }
}

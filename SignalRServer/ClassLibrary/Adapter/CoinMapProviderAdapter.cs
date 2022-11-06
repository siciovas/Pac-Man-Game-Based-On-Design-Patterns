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


        /* public ObservableCollection<Coin> GetVariousCoins(CoinFactory _otherFactory)
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
         }*/

        public ObservableCollection<Coin> GetFirstHalfCoins()
        {
            ObservableCollection<Coin> result = adaptee.GetCoins(10, 800, 50, 300); //perduodi ta pati kas pas tave cikle buvo nuo i iki i ir nuo j iki j
            /*for (int i = 10; i < 800; i = i + 50)
            {
                for (int j = 50; j < 300; j = j + 50)
                {
                    if (i == 10 && j == 50) continue;
                    var coin = _coinFactory.GetCoin(i, j);
                    result.Add(coin);
                }
            }*/
            return result;
        }

        public ObservableCollection<Coin> GetSecondHalfCoins()
        {
            ObservableCollection<Coin> result = adaptee.GetCoins(10, 800, 300, 600);
            /*for (int i = 10; i < 800; i = i + 50)
            {
                for (int j = 300; j < 600; j = j + 50)
                {
                    var coin = _coinFactory.GetCoin(i, j);
                    Coins.Add(coin);
                }
            }*/
            return Coins;
        }
    }
}

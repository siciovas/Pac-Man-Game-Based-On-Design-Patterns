using ClassLibrary.Coins.Factories;
using ClassLibrary.Coins.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Adapter.Interfaces
{
    public interface ICoinMapProvider
    {
        public ObservableCollection<Coin> GetFirstHalfCoins(CoinFactory coinFactory);

        public ObservableCollection<Coin> GetSecondHalfCoins(CoinFactory coinFactory);

    }
}

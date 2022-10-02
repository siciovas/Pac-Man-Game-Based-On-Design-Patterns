using System.Collections.ObjectModel;
using WPF.Game.Factory.Interfaces;

namespace WPF.Game.Factory.Classes
{
    public class CoinFactory
    {
        public ObservableCollection<ICoin> GetCoins(int level)
        {
            switch (level)
            {
                case 1:
                    return GetFirstLevelCoinMap();
                case 2:
                    return GetSecondLevelCoinMap();
                default:
                    return GetThirdLevelCoinMap();
            }
        }

        private ObservableCollection<ICoin> GetFirstLevelCoinMap()
        {
            ObservableCollection<ICoin> result = new ObservableCollection<ICoin>();
            for (int i = 10; i < 800; i = i + 50)
            {
                for (int j = 50; j < 800; j = j + 50)
                {
                    var coin = new BronzeCoin(i, j);
                    result.Add(coin);
                }
            }
            return result;
        }

        private ObservableCollection<ICoin> GetSecondLevelCoinMap()
        {
            ObservableCollection<ICoin> result = new ObservableCollection<ICoin>();
            for (int i = 10; i < 500; i = i + 50)
            {
                for (int j = 50; j < 200; j = j + 50)
                {
                    var coin = new SilverCoin(i, j);
                    result.Add(coin);
                }
            }
            for (int i = 10; i < 500; i = i + 50)
            {
                for (int j = 400; j < 800; j = j + 50)
                {
                    var coin = new SilverCoin(i, j);
                    result.Add(coin);
                }
            }
            for (int i = 650; i < 800; i = i + 50)
            {
                for (int j = 50; j < 300; j = j + 50)
                {
                    var coin = new SilverCoin(i, j);
                    result.Add(coin);
                }
            }
            return result;
            return result;
        }

        private ObservableCollection<ICoin> GetThirdLevelCoinMap()
        {
            ObservableCollection<ICoin> result = new ObservableCollection<ICoin>();
            for (int i = 10; i < 300; i = i + 50)
            {
                for (int j = 50; j < 200; j = j + 50)
                {
                    var coin = new GoldCoin(i, j);
                    result.Add(coin);
                }
            }
            for (int i = 10; i < 500; i = i + 50)
            {
                for (int j = 300; j < 550; j = j + 50)
                {
                    var coin = new GoldCoin(i, j);
                    result.Add(coin);
                }
            }
            for (int i = 650; i < 700; i = i + 50)
            {
                for (int j = 50; j < 250; j = j + 50)
                {
                    var coin = new GoldCoin(i, j);
                    result.Add(coin);
                }
            }

            for (int i = 750; i < 800; i = i + 50)
            {
                for (int j = 50; j < 600; j = j + 50)
                {
                    var coin = new GoldCoin(i, j);
                    result.Add(coin);
                }
            }
            return result;
        }
    }
}

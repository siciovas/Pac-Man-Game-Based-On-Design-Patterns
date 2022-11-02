using ClassLibrary.Coins.Factories;
using ClassLibrary.Coins.Interfaces;
using ClassLibrary.Fruits;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WPF.Game.Utils
{
    public class Utils
    {
        public static ObservableCollection<Coin> GetCoins(CoinFactory _coinFactory)
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

        public static ObservableCollection<Coin> GetFirstHalfCoins(CoinFactory _coinFactory)
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

        public static ObservableCollection<Coin> GetSecondHalfCoins(CoinFactory _coinFactory, ObservableCollection<Coin> Coins)
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
        }

        public static ObservableCollection<Apple> CreateApples()
        {
            ObservableCollection<Apple> result = new ObservableCollection<Apple>();
            var firstApple = new Apple(100, 300);
            var secondApple = new Apple(60, 450);
            result.Add(firstApple);
            result.Add(secondApple);
            return result;
        }

        public static ObservableCollection<RottenApple> CreateRottenApples()
        {
            ObservableCollection<RottenApple> result = new ObservableCollection<RottenApple>();
            var firstRottenApple = new RottenApple(150, 330);
            var secondRottenApple = new RottenApple(340, 110);
            result.Add(firstRottenApple);
            result.Add(secondRottenApple);
            return result;
        }

        public static ObservableCollection<Cherry> CreateCherries()
        {
            ObservableCollection<Cherry> result = new ObservableCollection<Cherry>();
            var firstCherry = new Cherry(30, 300);
            var secondCherry = new Cherry(390, 110);
            result.Add(firstCherry);
            result.Add(secondCherry);
            return result;
        }

        public static ObservableCollection<Strawberry> CreateStrawberries()
        {
            ObservableCollection<Strawberry> result = new ObservableCollection<Strawberry>();
            var firstStrawberry = new Strawberry(310, 400);
            var secondStrawberry = new Strawberry(500, 110);
            result.Add(firstStrawberry);
            result.Add(secondStrawberry);
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Game.AbstractFactory;
using WPF.Game.AbstractFactory.Interfaces;
using WPF.Game.Classes;
using WPF.Game.Factory.Classes;
using WPF.Game.Factory.Interfaces;

namespace WPF.Game.Utils
{
    public class Utils
    {
        public static ObservableCollection<ICoin> GetCoins(CoinFactory _coinFactory, ref List<ICoin> coinsList)
        {
            ObservableCollection<ICoin> result = new ObservableCollection<ICoin>();
            for (int i = 10; i < 800; i = i + 50)
            {
                for (int j = 50; j < 600; j = j + 50)
                {
                    if (i == 10 && j == 50) continue;
                    var coin = _coinFactory.GetCoin(i, j);
                    coinsList.Add(coin);
                    result.Add(coin);
                }
            }
            return result;
        }

        public static ObservableCollection<ICoin> GetFirstHalfCoins(CoinFactory _coinFactory, ref List<ICoin> coinsList)
        {
            ObservableCollection<ICoin> result = new ObservableCollection<ICoin>();
            for (int i = 10; i < 800; i = i + 50)
            {
                for (int j = 50; j < 300; j = j + 50)
                {
                    if (i == 10 && j == 50) continue;
                    var coin = _coinFactory.GetCoin(i, j);
                    result.Add(coin);
                    coinsList.Add(coin);
                }
            }
            return result;
        }

        public static ObservableCollection<ICoin> GetSecondHalfCoins(CoinFactory _coinFactory, ObservableCollection<ICoin> Coins, ref List<ICoin> coinsList)
        {
            //ObservableCollection<ICoin> result = new ObservableCollection<ICoin>();
            for (int i = 10; i < 800; i = i + 50)
            {
                for (int j = 300; j < 600; j = j + 50)
                {
                    var coin = _coinFactory.GetCoin(i, j);
                    Coins.Add(coin);
                    coinsList.Add(coin);
                }
            }
            return Coins;
        }

        public static ObservableCollection<Apple> CreateApples(ref List<Apple> ApplesList)
        {
            ObservableCollection<Apple> result = new ObservableCollection<Apple>();
            var firstApple = new Apple(100, 300);
            var secondApple = new Apple(60, 450);
            result.Add(firstApple);
            result.Add(secondApple);
            ApplesList.Add(firstApple);
            ApplesList.Add(secondApple);
            return result;
        }

        public static ObservableCollection<RottenApple> CreateRottenApples(ref List<RottenApple> RottenApplesList)
        {
            ObservableCollection<RottenApple> result = new ObservableCollection<RottenApple>();
            var firstRottenApple = new RottenApple(150, 330);
            var secondRottenApple = new RottenApple(340, 110);
            result.Add(firstRottenApple);
            result.Add(secondRottenApple);
            RottenApplesList.Add(firstRottenApple);
            RottenApplesList.Add(secondRottenApple);
            return result;
        }

        public static ObservableCollection<Cherry> CreateCherries(ref List<Cherry> CherriesList)
        {
            ObservableCollection<Cherry> result = new ObservableCollection<Cherry>();
            var firstCherry = new Cherry(30, 300);
            var secondCherry = new Cherry(390, 110);
            result.Add(firstCherry);
            result.Add(secondCherry);
            CherriesList.Add(firstCherry);
            CherriesList.Add(secondCherry);
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

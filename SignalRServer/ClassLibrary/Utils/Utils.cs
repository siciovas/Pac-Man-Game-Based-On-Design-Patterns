using ClassLibrary.Coins.Factories;
using ClassLibrary.Coins.Interfaces;
using ClassLibrary.Fruits;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ClassLibrary.Utils
{
    public class Utils
    {
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

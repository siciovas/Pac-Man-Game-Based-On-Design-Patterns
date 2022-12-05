using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ClassLibrary.MainUnit
{
    public class UnitFactory
    {
        private HashSet<Tuple<UnitType, string>> flyweights = new HashSet<Tuple<UnitType, string>>();

        public UnitFactory()
        {
            ImageBrush goldCoin = new ImageBrush();
            goldCoin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/goldCoin.png"));
            flyweights.Add(new Tuple<UnitType, string>(new UnitType(goldCoin, "Gold coin"), "goldCoin"));

            ImageBrush bronzeCoin = new ImageBrush();
            bronzeCoin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/bronzeCoin.png"));
            flyweights.Add(new Tuple<UnitType, string>(new UnitType(bronzeCoin, "Bronze coin"), "bronzeCoin"));

            ImageBrush silverCoin = new ImageBrush();
            silverCoin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/silverCoin.png"));
            flyweights.Add(new Tuple<UnitType, string>(new UnitType(silverCoin, "Silver coin"), "silverCoin"));

            ImageBrush pacmanBrush = new ImageBrush();
            pacmanBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/pacman.jpg"));
            flyweights.Add(new Tuple<UnitType, string>(new UnitType(pacmanBrush, "Pacman"), "pacman"));

            ImageBrush opponentBrush = new ImageBrush();
            pacmanBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/pacmanOp.jpg"));
            flyweights.Add(new Tuple<UnitType, string>(new UnitType(opponentBrush, "Opponent"), "opponent"));
        }

        public UnitType GetFlyweight(string key)
        {
            if (flyweights.Where(t => t.Item2 == key).Count() == 0)
            {
                Console.WriteLine("FlyweightFactory: Can't find a flyweight, creating new one.");
                this.flyweights.Add(new Tuple<UnitType, string>(new UnitType(), key));
            }
            else
            {
                Console.WriteLine("FlyweightFactory: Reusing existing flyweight.");
            }
            return this.flyweights.Where(t => t.Item2 == key).FirstOrDefault().Item1;
        }
    }
}

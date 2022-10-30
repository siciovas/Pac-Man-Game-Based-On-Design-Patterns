using ClassLibrary.MainUnit;
using ClassLibrary.Strategies;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System;
using ClassLibrary.Fruits;

namespace ClassLibrary._Pacman
{
    public class Pacman : Unit
    {
        public int Speed { get; set; }
        public int Score { get; set; }
        public int Health { get; set; }

        private Algorithm algorithm;

        public Algorithm GetAlgorithm()
        {
            return algorithm;
        }

        public void SetAlgorithm(Algorithm algorithm)
        {
            this.algorithm = algorithm;
        }

        public Pacman(string name) 
        {
            Speed = 8;
            Score = 0;
            Health = 100;
            Name = name;
            ImageBrush pacmanBrush = new ImageBrush();
            pacmanBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/pacman.jpg"));
            Appearance = pacmanBrush;
          
        }

        public void Action(ref Pacman pacman)
        {
            algorithm.BehaveDifferently(ref pacman);
        }

        public override Pacman Copy()
        {
            //creates a green copy of pacman using deep copy
            var clone = (Pacman)this.MemberwiseClone();
            clone.algorithm = new DefaultAlgorithm();
            clone.Appearance = new ImageBrush();
            ImageBrush pacmanBrush = new ImageBrush();
            pacmanBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/pacmanOp.jpg"));
            clone.Appearance = pacmanBrush;
            clone.Name = "Opponent";
            return clone;
        }
    }
}

using ClassLibrary.MainUnit;
using ClassLibrary.Strategies;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System;

namespace ClassLibrary._Pacman
{
    public class Pacman : Unit
    {
        public int Speed { get; set; }
        public int Score { get; set; }

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
            Name = name;
            if(name == "Pacman")
            {
                ImageBrush pacmanBrush = new ImageBrush();
                pacmanBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/pacman.jpg"));
                Appearance = pacmanBrush;
            } else
            {
                ImageBrush pacmanBrush = new ImageBrush();
                pacmanBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/pacmanOp.jpg"));
                Appearance = pacmanBrush;
            }
        }

        public void Action(ref Pacman pacman)
        {
            algorithm.BehaveDifferently(ref pacman);
        }
    }
}

using ClassLibrary.MainUnit;
using ClassLibrary.Strategies;

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

        public Pacman() 
        {
            Speed = 8;
            Score = 0;
            Name = "Pacman";
        }

        public void Action(ref Pacman pacman)
        {
            algorithm.BehaveDifferently(ref pacman);
        }
    }
}

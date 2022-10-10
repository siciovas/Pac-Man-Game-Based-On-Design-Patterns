using ClassLibrary.Pacmen;

namespace ClassLibrary.Strategies
{
    public class DoublePoints : Algorithm
    {
        public override void BehaveDifferently(ref Pacman pacman)
        {
            pacman.Score *= 2;
        }
    }
}

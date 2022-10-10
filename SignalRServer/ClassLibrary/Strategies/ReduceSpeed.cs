using ClassLibrary.Pacmen;

namespace ClassLibrary.Strategies
{
    public class ReduceSpeed : Algorithm
    {
        public override void BehaveDifferently(ref Pacman pacman)
        {
            pacman.Speed = 4;
        }
    }
}

using ClassLibrary._Pacman;

namespace ClassLibrary.Strategies
{
    public class GiveSpeed : Algorithm
    {
        public override void BehaveDifferently(ref Pacman pacman)
        {
            pacman.Speed = 16;
        }
    }
}

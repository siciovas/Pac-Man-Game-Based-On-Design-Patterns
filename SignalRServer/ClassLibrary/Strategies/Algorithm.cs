using ClassLibrary._Pacman;

namespace ClassLibrary.Strategies
{
    public abstract class Algorithm
    {
        public Algorithm()
        {

        }

        public abstract void BehaveDifferently(ref Pacman pacman);
    }
}

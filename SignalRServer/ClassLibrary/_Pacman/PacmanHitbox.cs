using System.Windows;

namespace ClassLibrary._Pacman
{
    public class PacmanHitbox
    {
        private PacmanHitbox()
        {
        }

        private static PacmanHitbox _instance = null;
        private static readonly object threadLock = new object();

        public static PacmanHitbox GetInstance
        {
            get
            {
                lock (threadLock)
                {
                    if (_instance == null)
                    {
                        _instance = new PacmanHitbox();
                    }
                }
                return _instance;
            }
        }

        public Rect GetCurrentHitboxPosition(double x, double y, double width, double height)
        {
            return new Rect(x, y, width, height);
        }

    }
}

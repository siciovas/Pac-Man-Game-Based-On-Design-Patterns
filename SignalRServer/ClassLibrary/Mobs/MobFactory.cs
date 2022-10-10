using ClassLibrary.Mobs.Interfaces;

namespace ClassLibrary.Mobs
{
    public abstract class MobFactory
    {
        public abstract IGhost CreateGhost(int top, int left);
        public abstract IZombie CreateZombie(int top, int left);
        public abstract IDemogorgon CreateDemogorgon();
    }
}

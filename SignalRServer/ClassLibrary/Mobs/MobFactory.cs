using ClassLibrary.Mobs.Interfaces;

namespace ClassLibrary.Mobs
{
    public abstract class MobFactory
    {
        protected MobDirector m_Director = new MobDirector();
        public abstract Mob CreateGhost(int top, int left);
        public abstract Mob CreateZombie(int top, int left);
        public abstract Mob CreateDemogorgon(int top, int left);
    }
}

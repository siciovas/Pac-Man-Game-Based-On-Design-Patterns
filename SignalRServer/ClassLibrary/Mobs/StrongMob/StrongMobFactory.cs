using ClassLibrary.Mobs.Interfaces;

namespace ClassLibrary.Mobs.StrongMob
{
    public class StrongMobFactory : MobFactory
    {
        public override Mob CreateGhost(int top, int left)
        {
            Mob rawMob = new StrongGhost(top, left, "ghost");
            GhostBuilder builder = new GhostBuilder(rawMob);
            m_Director.CreateSlowAndStrongMob(builder);
            return m_Director.Build(builder);
        }

        public override Mob CreateZombie(int top, int left)
        {
            Mob rawMob = new StrongZombie(top, left, "zombie");
            ZombieBuilder builder = new ZombieBuilder(rawMob);
            m_Director.CreateSlowAndStrongMob(builder);
            return m_Director.Build(builder);
        }
        public override Mob CreateDemogorgon(int top, int left)
        {
            Mob rawMob = new StrongDemogorgon(top, left, "demogorgon");
            DemogorgonBuilder builder = new DemogorgonBuilder(rawMob);
            m_Director.CreateFastAndStrongMob(builder);
            return m_Director.Build(builder);
        }
    }
}

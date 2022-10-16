namespace ClassLibrary.Mobs.WeakMob
{
    public class WeakMobFactory : MobFactory
    {
        public override Mob CreateGhost(int top, int left)
        {
            Mob rawMob = new WeakGhost(top, left, "ghost");
            GhostBuilder builder = new GhostBuilder(rawMob);
            m_Director.CreateSlowAndWeakMob(builder);
            return m_Director.Build(builder);
        }

        public override Mob CreateZombie(int top, int left)
        {
            Mob rawMob = new WeakZombie(top, left, "zombie");
            ZombieBuilder builder = new ZombieBuilder(rawMob);
            m_Director.CreateSlowAndWeakMob(builder);
            return m_Director.Build(builder);
        }
        public override Mob CreateDemogorgon(int top, int left)
        {
            Mob rawMob = new WeakDemogorgon(top, left, "demogorgon");
            DemogorgonBuilder builder = new DemogorgonBuilder(rawMob);
            m_Director.CreateFastAndWeakMob(builder);
            return m_Director.Build(builder);
        }
    }
}

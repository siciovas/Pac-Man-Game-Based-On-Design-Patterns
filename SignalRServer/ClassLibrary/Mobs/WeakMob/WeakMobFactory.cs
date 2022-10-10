using ClassLibrary.Mobs.Interfaces;

namespace ClassLibrary.Mobs.WeakMob
{
    public class WeakMobFactory : MobFactory
    {
        public override IGhost CreateGhost(int top, int left)
        {
            return new WeakGhost(top, left);
        }

        public override IZombie CreateZombie(int top, int left)
        {
            return new WeakZombie(top, left);
        }
        public override IDemogorgon CreateDemogorgon()
        {
            return new WeakDemogorgon();
        }
    }
}

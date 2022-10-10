using ClassLibrary.Mobs.Interfaces;

namespace ClassLibrary.Mobs.StrongMob
{
    public class StrongMobFactory : MobFactory
    {
        public override IGhost CreateGhost(int top, int left)
        {
            return new StrongGhost(top, left);
        }

        public override IZombie CreateZombie(int top, int left) => 
            new StrongZombie(top, left);
        public override IDemogorgon CreateDemogorgon()
        {
            return new StrongDemogorgon();
        }
    }
}

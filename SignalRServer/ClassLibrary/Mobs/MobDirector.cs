using ClassLibrary.Mobs.Interfaces;

namespace ClassLibrary.Mobs
{
    public class MobDirector
    {
        public void CreateSlowAndWeakMob(IBuilder builder)
        {
            builder.SetSpeed(3);
            builder.SetDamage(3);
        }

        public void CreateFastAndWeakMob(IBuilder builder)
        {
            builder.SetSpeed(6);
            builder.SetDamage(3);
        }

        public void CreateSlowAndStrongMob(IBuilder builder)
        {
            builder.SetSpeed(3);
            builder.SetDamage(6);
        }

        public void CreateFastAndStrongMob(IBuilder builder)
        {
            builder.SetSpeed(6);
            builder.SetDamage(6);
        }

        public Mob Build(IBuilder builder)
        {
            return builder.Build();
        }

    }
}

using ClassLibrary.Mobs.Interfaces;

namespace ClassLibrary.Mobs
{
    public class DemogorgonBuilder : IBuilder
    {
        public DemogorgonBuilder(Mob rawMob) : base(rawMob)
        {
        }

        public override void SetDamage(int Damage)
        {
            rawMob.SetDamage(Damage);
        }

        public override void SetSpeed(int Speed)
        {
            rawMob.SetSpeed(Speed);
        }
    }
}

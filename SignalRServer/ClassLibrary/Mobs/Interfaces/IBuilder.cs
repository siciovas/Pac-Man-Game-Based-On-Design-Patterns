namespace ClassLibrary.Mobs.Interfaces
{
    public abstract class IBuilder
    {
        protected Mob rawMob;

        public IBuilder(Mob rawMob)
        {
            this.rawMob = rawMob;
        }

        public abstract void SetSpeed(int Speed);
        public abstract void SetDamage(int Damage);
        public Mob Build()
        {
            return rawMob;
        }
    }
}

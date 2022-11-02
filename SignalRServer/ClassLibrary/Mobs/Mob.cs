using ClassLibrary.MainUnit;

namespace ClassLibrary.Mobs
{
    public abstract class Mob : Unit
    {
        private int Speed;
        private int Damage;
        public bool GoLeft;

        public int GetSpeed()
        {
            return Speed;
        }
        public int GetDamage()
        {
            return Damage;
        }

        public void SetSpeed(int speed)
        {
            Speed = speed;
        }

        public void SetDamage(int damage)
        {
            Damage = damage;
        }
    }
}

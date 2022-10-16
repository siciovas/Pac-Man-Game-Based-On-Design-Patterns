namespace ClassLibrary.Mobs
{
    public abstract class Mob
    {
        private string Name = "";
        private int Speed;
        private int Damage;

        public Mob(string name)
        {
            Name = name;
        }

        public string GetName()
        {
            return Name;
        }

        public int GetSpeed()
        {
            return Speed;
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

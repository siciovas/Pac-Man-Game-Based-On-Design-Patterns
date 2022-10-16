using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Mobs
{
    public abstract class Mob
    {
        private string Name = "";
        private int Speed;
        private int Damage;
        private int Top;
        private int Left;

        public Mob(string name, int top, int left)
        {
            Name = name;
            Top = top;
            Left = left;
        }

        public string GetName() 
        {
            return Name;
        }

        public int GetTop()
        {
            return Top;
        }

        public int GetLeft()
        {
            return Left;
        }

        public void SetTop(int top)
        {
            Top += top;
        }

        public void SetLeft(int left) {
            Left += left;
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

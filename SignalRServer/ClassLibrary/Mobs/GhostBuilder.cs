using ClassLibrary.Mobs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Mobs
{
    public class GhostBuilder : IBuilder
    {
        public GhostBuilder(Mob rawMob) : base(rawMob)
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

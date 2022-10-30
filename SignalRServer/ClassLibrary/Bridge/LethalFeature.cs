using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Bridge
{
    public class LethalFeature : IFeature
    {
        public override int GetDamage()
        {
            return Damage;
        }

        public override void SetDamage()
        {
            Damage = 100;
        }
    }
}

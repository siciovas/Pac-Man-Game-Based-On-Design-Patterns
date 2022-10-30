using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Bridge
{
    public abstract class IFeature
    {
        protected int Damage;
        public abstract void SetDamage();
        public abstract int GetDamage();
    }
}

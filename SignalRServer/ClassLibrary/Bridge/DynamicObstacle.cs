using ClassLibrary.MainUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Bridge
{
    public abstract class DynamicObstacle : Unit
    {
        protected IFeature Feature;
        public bool GoLeft;

        protected DynamicObstacle(IFeature feature)
        {
            UnitType = new UnitType();

            Feature = feature;
        }
        public abstract void SetDamage();
    }
}

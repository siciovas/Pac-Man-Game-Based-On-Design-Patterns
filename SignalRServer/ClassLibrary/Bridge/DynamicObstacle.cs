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

        protected DynamicObstacle(IFeature feature)
        {
            Feature = feature;
        }
        public abstract void SetDamage();
    }
}

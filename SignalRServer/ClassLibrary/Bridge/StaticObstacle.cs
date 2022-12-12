using ClassLibrary.MainUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ClassLibrary.Bridge
{
    public abstract class StaticObstacle : Unit
    {
        protected IFeature Feature;

        protected StaticObstacle(IFeature feature)
        {
            UnitType = new UnitType();

            Feature = feature;
        }

        public abstract void SetDamage();
    }
}

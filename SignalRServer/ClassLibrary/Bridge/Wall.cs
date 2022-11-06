using ClassLibrary.MainUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Bridge
{
    public class Wall : StaticObstacle
    {
        public Wall(IFeature feature) : base(feature)
        {
        }

        public override Unit Copy()
        {
            throw new NotImplementedException();
        }

        public override void SetDamage()
        {
            Feature.SetDamage();
        }
        public int GetDamage()
        {
            return Feature.GetDamage();
        }
    }
}

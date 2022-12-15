using ClassLibrary.MainUnit;
using ClassLibrary.Mobs;
using ClassLibrary.Mobs.StrongMob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Visitor
{
    public class MobVisitor : IVisitor
    {
        public void Visit(Unit unit)
        {
            MobFactory mobFactory = new StrongMobFactory();
            Unit strongMob = mobFactory.CreateDemogorgon(unit.Top, unit.Left);
            unit.UnitType = strongMob.UnitType;
        }
    }
}

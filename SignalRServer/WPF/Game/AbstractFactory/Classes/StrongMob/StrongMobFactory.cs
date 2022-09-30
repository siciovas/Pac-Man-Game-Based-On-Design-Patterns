using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Game.AbstractFactory.Classes.StrongMob;
using WPF.Game.AbstractFactory.Interfaces;

namespace WPF.Game.AbstractFactory.Classes.StrongMobFactory
{
    public class StrongMobFactory : MobFactory
    {
        public override IGhost CreateGhost()
        {
            return new StrongGhost();
        }

        public override IZombie CreateZombie()
        {
            return new StrongZombie();
        }
        public override IDemogorgon CreateDemogorgon()
        {
            return new StrongDemogorgon();
        }
    }
}

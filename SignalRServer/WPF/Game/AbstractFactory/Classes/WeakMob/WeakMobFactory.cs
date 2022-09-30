using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Game.AbstractFactory.Classes.WeakMob;
using WPF.Game.AbstractFactory.Interfaces;

namespace WPF.Game.AbstractFactory.Classes.WeakMobFactory
{
    public class WeakMobFactory : MobFactory
    {
        public override IGhost CreateGhost()
        {
            return new WeakGhost();
        }

        public override IZombie CreateZombie()
        {
            return new WeakZombie();
        }
        public override IDemogorgon CreateDemogorgon()
        {
            throw new NotImplementedException();
        }
    }
}

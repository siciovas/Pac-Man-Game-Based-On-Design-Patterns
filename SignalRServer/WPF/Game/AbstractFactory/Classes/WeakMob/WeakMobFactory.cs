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
        public override IGhost CreateGhost(int top, int left)
        {
            return new WeakGhost(top, left);
        }

        public override IZombie CreateZombie()
        {
            return new WeakZombie();
        }
        public override IDemogorgon CreateDemogorgon()
        {
            return new WeakDemogorgon();
        }
    }
}

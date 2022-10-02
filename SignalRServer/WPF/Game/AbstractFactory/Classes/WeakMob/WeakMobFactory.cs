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

        public override IZombie CreateZombie(int top, int left)
        {
            return new WeakZombie(top, left);
        }
        public override IDemogorgon CreateDemogorgon()
        {
            return new WeakDemogorgon();
        }
    }
}

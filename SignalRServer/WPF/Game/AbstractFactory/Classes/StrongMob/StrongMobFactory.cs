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
        public override IGhost CreateGhost(int top, int left)
        {
            return new StrongGhost(top, left);
        }

        public override IZombie CreateZombie(int top, int left)
        {
            return new StrongZombie(top, left);
        }
        public override IDemogorgon CreateDemogorgon()
        {
            return new StrongDemogorgon();
        }
    }
}

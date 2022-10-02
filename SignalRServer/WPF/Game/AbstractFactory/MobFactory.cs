using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Game.AbstractFactory.Interfaces;

namespace WPF.Game.AbstractFactory
{
    public abstract class MobFactory
    {
        public abstract IGhost CreateGhost(int top, int left);
        public abstract IZombie CreateZombie();
        public abstract IDemogorgon CreateDemogorgon();
    }
}

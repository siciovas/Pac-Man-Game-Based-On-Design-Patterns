using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Game.AbstractFactory.Interfaces;

namespace WPF.Game.AbstractFactory.Classes.WeakMob
{
    public class WeakGhost : IGhost
    {
        public int Speed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}

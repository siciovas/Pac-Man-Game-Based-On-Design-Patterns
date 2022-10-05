using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Game.Classes;

namespace WPF.Game.Strategy
{
    public abstract class Algorithm
    {
        public abstract void BehaveDifferently(ref Pacman pacman);
    }
}

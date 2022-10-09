using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Game.Classes;

namespace WPF.Game.Strategy
{
    internal class DoublePoints : Algorithm
    {
        public override void BehaveDifferently(ref Pacman pacman)
        {
            pacman.Score *= 2;
        }
    }
}

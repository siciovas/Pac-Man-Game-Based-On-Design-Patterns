using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Game.Classes;

namespace WPF.Game.Strategy
{
    public class GiveSpeed : Algorithm
    {
        public override void BehaveDifferently(ref Pacman pacman)
        {
            pacman.Speed = 16;
        }
    }
}

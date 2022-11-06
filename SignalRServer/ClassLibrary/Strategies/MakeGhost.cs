using ClassLibrary._Pacman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Strategies
{
    public class MakeGhost : Algorithm
    {
        public override void BehaveDifferently(ref Pacman pacman)
        {
            pacman.GhostMode = true;
        }
    }
}

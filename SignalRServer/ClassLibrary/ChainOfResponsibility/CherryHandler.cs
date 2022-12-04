﻿using ClassLibrary._Pacman;
using ClassLibrary.Fruits;
using ClassLibrary.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.ChainOfResponsibility
{
    public class CherryHandler : AbstractHandler
    {
        public override void Handle(ref Pacman request, object fruit)
        {
            if (fruit.GetType() == typeof(Cherry))
            {
                request.SetAlgorithm(new DoublePoints());
                request.Action(ref request);
            }
            else
            {
                base.Handle(ref request, fruit);
            }
        }
    }
}

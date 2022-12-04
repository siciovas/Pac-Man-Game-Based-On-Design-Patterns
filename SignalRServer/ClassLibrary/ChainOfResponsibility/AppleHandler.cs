using ClassLibrary.Fruits;
using ClassLibrary.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary._Pacman;

namespace ClassLibrary.ChainOfResponsibility
{
    public class AppleHandler : AbstractHandler
    {
        public override void Handle(ref Pacman request, object fruit)
        {
            if (fruit.GetType() == typeof(Apple))
            {
                request.SetAlgorithm(new GiveSpeed());
                request.Action(ref request);
            }
            else
            {
                base.Handle(ref request, fruit);
            }
        }
    }
}

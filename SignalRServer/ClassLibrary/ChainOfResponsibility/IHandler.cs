using ClassLibrary._Pacman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.ChainOfResponsibility
{
    public interface IHandler
    {
        public IHandler SetNext(IHandler handler);

        public void Handle(ref Pacman request, object fruit);
    }
}

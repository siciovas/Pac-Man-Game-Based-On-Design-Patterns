using ClassLibrary._Pacman;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.ChainOfResponsibility
{
    public abstract class AbstractHandler : IHandler
    {
        public IHandler _nextHandler;
        public virtual void Handle(ref Pacman request, object fruit)
        {
            if (this._nextHandler != null)
            {
                this._nextHandler.Handle(ref request, fruit);
            }
            else
            {
                
            }
        }

        public IHandler SetNext(IHandler handler)
        {
            this._nextHandler = handler;
            return handler;
        }
    }
}

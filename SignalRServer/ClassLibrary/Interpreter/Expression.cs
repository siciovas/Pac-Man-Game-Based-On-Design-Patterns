using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Interpreter
{
    public abstract class Expression
    {
        public abstract void Interpret(object _object, HubConnection connection);
    }
}

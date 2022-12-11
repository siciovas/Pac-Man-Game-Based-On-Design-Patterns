using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Memento
{
    public interface IMemento
    {
        (int, int) GetState();
    }
}

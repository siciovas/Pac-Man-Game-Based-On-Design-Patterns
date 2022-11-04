using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Adapter.Interfaces
{
    public interface IConnectionAdapter
    {
        public void On<T>(string name, Action action);
        public Task Invoke(string coordinates, object obj);
    }
}

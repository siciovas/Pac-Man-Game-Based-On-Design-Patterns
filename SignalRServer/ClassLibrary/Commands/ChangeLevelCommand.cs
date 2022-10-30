using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Commands
{
    public class ChangeLevelCommand : ICommand<Action>
    {
        public ChangeLevelCommand()
        {
        }

        public void Execute(Action item)
        {
            item?.Invoke();
        }
    }
}

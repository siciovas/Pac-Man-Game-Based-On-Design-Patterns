using ClassLibrary.Fruits;
using System;
using System.Collections.ObjectModel;

namespace ClassLibrary.Commands
{
    internal class CherriesIndexCommand : ICommand<ObservableCollection<Cherry>>
    {
        public void Execute()
        {
            throw new NotImplementedException();
        }

        public void Execute(ObservableCollection<Cherry> item)
        {
            throw new NotImplementedException();
        }
    }
}

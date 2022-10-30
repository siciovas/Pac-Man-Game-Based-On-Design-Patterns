using ClassLibrary.Coins.Interfaces;
using System;
using System.Collections.ObjectModel;

namespace ClassLibrary.Commands
{
    internal class CoinsIndexCommand : ICommand<ObservableCollection<Coin>>
    {
        public void Execute()
        {
            throw new NotImplementedException();
        }

        public void Execute(ObservableCollection<Coin> item)
        {
            throw new NotImplementedException();
        }
    }
}

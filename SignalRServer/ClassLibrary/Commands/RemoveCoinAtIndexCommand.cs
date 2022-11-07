using ClassLibrary.Coins.Interfaces;
using System.Collections.ObjectModel;

namespace ClassLibrary.Commands
{
    public class RemoveCoinAtIndexCommand : ICommand
    {
        public int Index { get; set; }

        public RemoveCoinAtIndexCommand(int index)
        {
            Index = index;
        }

        public void Execute(object parameter)
        {
            var coins = (ObservableCollection<Coin>)parameter;
            coins.RemoveAt(Index);
        }
    }
}

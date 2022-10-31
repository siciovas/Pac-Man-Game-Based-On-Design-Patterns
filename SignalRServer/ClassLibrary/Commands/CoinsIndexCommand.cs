using ClassLibrary.Coins.Interfaces;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace ClassLibrary.Commands
{
    public class CoinsIndexCommand : ICommand
    {
        public int Index { get; set; }
        [JsonConstructor]
        public CoinsIndexCommand(int index)
        {
            Index = index;
        }
        public override void Execute(object parameter)
        {
            var coins = (ObservableCollection<Coin>)parameter;
            coins.RemoveAt(Index);
        }
    }
}

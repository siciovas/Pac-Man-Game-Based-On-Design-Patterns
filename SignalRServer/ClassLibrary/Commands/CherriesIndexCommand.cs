using ClassLibrary.Fruits;
using System.Collections.ObjectModel;

namespace ClassLibrary.Commands
{
    public class CherriesIndexCommand : ICommand
    {
        public int Index { get; set; }
        public CherriesIndexCommand(int index)
        {
            Index = index;
        }
        public override void Execute(object parameter)
        {
            var cherries = (ObservableCollection<Cherry>)parameter;
            cherries.RemoveAt(Index);
        }
    }
}

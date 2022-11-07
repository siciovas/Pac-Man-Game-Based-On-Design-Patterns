using ClassLibrary.Fruits;
using System.Collections.ObjectModel;

namespace ClassLibrary.Commands
{
    public class RemoveStrawberryAtIndexCommand : ICommand
    {
        public int Index { get; set; }

        public RemoveStrawberryAtIndexCommand(int index)
        {
            Index = index;
        }
        public void Execute(object parameter)
        {
            var strawberries = (ObservableCollection<Strawberry>)parameter;
            strawberries.RemoveAt(Index);
        }

        public void Undo(object parameter, Strawberry strawberry)
        {
            var strawberries = (ObservableCollection<Strawberry>)parameter;
            strawberries.Insert(Index, strawberry);
        }
    }
}

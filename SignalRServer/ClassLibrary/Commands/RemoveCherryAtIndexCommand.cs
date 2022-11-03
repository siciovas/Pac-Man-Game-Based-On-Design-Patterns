using ClassLibrary.Fruits;
using System;
using System.Collections.ObjectModel;

namespace ClassLibrary.Commands
{
    public class RemoveCherryAtIndexCommand : ICommand
    {
        public int Index { get; set; }

        public RemoveCherryAtIndexCommand(int index)
        {
            Index = index;
        }

        public void Execute(object parameter)
        {
            var cherries = (ObservableCollection<Cherry>)parameter;
            cherries.RemoveAt(Index);
        }

        public void Undo(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}

using ClassLibrary.Fruits;
using System;
using System.Collections.ObjectModel;

namespace ClassLibrary.Commands
{
    public class RemoveAppleAtIndexCommand : ICommand
    {
        public int Index { get; set; }

        public RemoveAppleAtIndexCommand(int index)
        {
            Index = index;
        }
        public void Execute(object parameter)
        {
            var apples = (ObservableCollection<Apple>)parameter;
            apples.RemoveAt(Index);
        }

        public void Undo(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}

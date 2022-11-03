using ClassLibrary.Fruits;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void Undo(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}

using ClassLibrary.Fruits;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Commands
{
    public class AddCherryCommand : ICommand
    {
        private List<Cherry> _CherriesList;

        public AddCherryCommand(int index)
        {
            Index = index;
        }

        public int Index { get; }

        public void SetCherriesListCopy(List<Cherry> list)
        {
            _CherriesList = list;
        }

        public void Execute(object parameter)
        {
            var cherries = (ObservableCollection<Cherry>)parameter;
            cherries.Add(_CherriesList[Index]);
        }

        public void Undo(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}

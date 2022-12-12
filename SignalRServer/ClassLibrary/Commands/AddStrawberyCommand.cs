using ClassLibrary.Fruits;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Commands
{
    public class AddStrawberyCommand
    {
        private List<Strawberry> _StrawberriesList;

        public AddStrawberyCommand(int index)
        {
            Index = index;
        }

        public int Index { get; }

        public void SetStrawberriesListCopy(List<Strawberry> list)
        {
            _StrawberriesList = list;
        }

        public void Execute(object parameter)
        {
            var strawberries = (ObservableCollection<Strawberry>)parameter;
            strawberries.Add(_StrawberriesList[Index]);
        }

        public void Undo(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}

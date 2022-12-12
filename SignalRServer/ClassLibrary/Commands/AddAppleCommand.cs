using ClassLibrary.Fruits;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Commands
{
    public class AddAppleCommand : ICommand
    {
        private List<Apple> _ApplesList;

        public AddAppleCommand(int index)
        {
            Index = index;
        }

        public int Index { get; }

        public void SetApplesListCopy(List<Apple> list)
        {
            _ApplesList = list;
        }

        public void Execute(object parameter)
        {
            var apples = (ObservableCollection<Apple>)parameter;
            apples.Add(_ApplesList[Index]);
        }

        public void Undo(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}

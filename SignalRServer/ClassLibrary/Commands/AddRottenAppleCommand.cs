using ClassLibrary.Fruits;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Commands
{
    public class AddRottenAppleCommand : ICommand
    {
        private List<RottenApple> _RottenApplesList;

        public AddRottenAppleCommand(int index)
        {
            Index = index;
        }

        public int Index { get; }

        public void SetRottenApplesListCopy(List<RottenApple> list)
        {
            _RottenApplesList = list;
        }

        public void Execute(object parameter)
        {
            var rottenApples = (ObservableCollection<RottenApple>)parameter;
            rottenApples.Add(_RottenApplesList[Index]);
        }

        public void Undo(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}

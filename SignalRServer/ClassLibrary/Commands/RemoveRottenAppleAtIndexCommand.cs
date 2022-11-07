using ClassLibrary.Fruits;
using System;
using System.Collections.ObjectModel;

namespace ClassLibrary.Commands
{
    public class RemoveRottenAppleAtIndexCommand : ICommand
    {
        public int Index { get; set; }

        public RemoveRottenAppleAtIndexCommand(int index)
        {
            Index = index;
        }

        public void Execute(object parameter)
        {
            var rottenApples = (ObservableCollection<RottenApple>)parameter;
            rottenApples.RemoveAt(Index);
        }
    }
}

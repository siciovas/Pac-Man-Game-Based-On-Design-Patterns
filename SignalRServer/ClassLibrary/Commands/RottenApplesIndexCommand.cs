using ClassLibrary.Fruits;
using System.Collections.ObjectModel;

namespace ClassLibrary.Commands
{
    public class RottenApplesIndexCommand : ICommand
    {
        public int Index { get; set; }
        public RottenApplesIndexCommand(int index)
        {
            Index = index;
        }
        public override void Execute(object parameter)
        {
            var rottenApples = (ObservableCollection<RottenApple>)parameter;
            rottenApples.RemoveAt(Index);
        }
    }
}

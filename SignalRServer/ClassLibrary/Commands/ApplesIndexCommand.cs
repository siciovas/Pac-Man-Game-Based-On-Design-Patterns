using ClassLibrary.Fruits;
using System.Collections.ObjectModel;

namespace ClassLibrary.Commands
{
    public class ApplesIndexCommand : ICommand
    {
        public int Index { get; set; }
        public ApplesIndexCommand(int index)
        {
            Index = index;
        }
        public override void Execute(object parameter)
        {
            var apples = (ObservableCollection<Apple>)parameter;
            apples.RemoveAt(Index);
        }
    }
}

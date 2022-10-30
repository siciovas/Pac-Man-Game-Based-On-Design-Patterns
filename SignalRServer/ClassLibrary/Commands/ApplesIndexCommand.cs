using ClassLibrary.Fruits;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ClassLibrary.Commands
{
    public class ApplesIndexCommand : ICommand<ObservableCollection<Apple>>
    {
        public int Index { get; set; }
        public ObservableCollection<Apple> Apples { get; set; }

        public ApplesIndexCommand(int index)
        {
            Index = index;
        }

        public void Execute(ObservableCollection<Apple> item)
        {
            item.RemoveAt(Index);
        }
    }
}

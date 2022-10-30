using ClassLibrary.Fruits;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ClassLibrary.Commands
{
    public class ApplesIndexCommand
    {
        public int Index { get; set; }

        public ApplesIndexCommand(int index)
        {
            Index = index;
        }

        public void Execute(ref ObservableCollection<Apple> apples, ref List<Apple> applesList)
        {
            apples.RemoveAt(Index);
            applesList.RemoveAt(Index);
        }
    }
}

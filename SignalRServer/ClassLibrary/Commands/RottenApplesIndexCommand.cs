using ClassLibrary.Fruits;
using System;
using System.Collections.ObjectModel;

namespace ClassLibrary.Commands
{
    internal class RottenApplesIndexCommand : ICommand<ObservableCollection<RottenApple>>
    {
        public void Execute()
        {
            throw new NotImplementedException();
        }

        public void Execute(ObservableCollection<RottenApple> item)
        {
            throw new NotImplementedException();
        }
    }
}

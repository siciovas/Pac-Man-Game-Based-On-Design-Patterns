using System;

namespace ClassLibrary.Commands
{
    public class ChangeLevelCommand : ICommand
    {
        public ChangeLevelCommand()
        {
        }

        public override void Execute(object parameter)
        {
            var item = (Action)parameter;
            item?.Invoke();
        }
    }
}

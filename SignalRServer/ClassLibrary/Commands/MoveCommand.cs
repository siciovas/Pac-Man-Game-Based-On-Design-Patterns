using System;

namespace ClassLibrary.Commands
{
    public class MoveCommand : ICommand
    {
        public string SerializedObject { get; set; }

        public MoveCommand(string serializedObject)
        {
            SerializedObject = serializedObject;
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Undo(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}

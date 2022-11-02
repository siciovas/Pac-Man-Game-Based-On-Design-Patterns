namespace ClassLibrary.Commands
{
    public interface ICommand
    {
        void Execute(object parameter);

        void Undo(object parameter);
    }
}

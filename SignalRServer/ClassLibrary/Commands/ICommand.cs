namespace ClassLibrary.Commands
{
    public interface ICommand<T>
    {
        void Execute(T item);
    }
}

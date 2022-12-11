namespace ClassLibrary.Memento
{
    public class ConcreteMemento : IMemento
    {
        private (int, int) _state;

        public ConcreteMemento(int top, int left)
        {
            _state = (top, left);
        }
        public (int, int) GetState()
        {
            return _state;
        }
    }
}

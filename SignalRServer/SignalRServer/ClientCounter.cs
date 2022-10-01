namespace SignalRServer
{
    public class ClientCounter
    {
        int _counter;

        public int GetCount() => _counter;

        public void AddClient() => _counter++;

        public void RemoveClient() => _counter--;
    }
}

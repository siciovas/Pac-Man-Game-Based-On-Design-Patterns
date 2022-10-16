namespace SignalRServer
{
    public class ClientCounter
    {
        int _counter;
        List<string> _clients = new();
        public int GetCount() => _counter;
        public List<string> GetClients() => _clients;

        public void AddClient(string connectionId)
        {
            _counter++;
            _clients.Add(connectionId);
        }

        public void RemoveClient(string connectionId)
        {
            _counter--;
            _clients.Remove(connectionId);
        }
    }
}

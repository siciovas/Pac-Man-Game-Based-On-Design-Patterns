namespace SignalRServer
{
    public sealed class ClientCounter
    {
        private static readonly ClientCounter _instance;

        private int _counter;
        private List<string> _clients;
        static ClientCounter()
        {
            _instance = new ClientCounter();
        }

        private ClientCounter()
        {
            _counter = 0;
            _clients = new();
        }

        public static ClientCounter Instance
        {
            get
            {
                return _instance;
            }
        }
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

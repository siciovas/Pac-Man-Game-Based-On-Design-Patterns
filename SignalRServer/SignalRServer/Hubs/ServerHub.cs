using ClassLibrary.Observer;
using Microsoft.AspNetCore.SignalR;

namespace SignalRServer.Hubs
{
    public class ServerHub : Hub, ISubject
    {
        private ClientCounter _clientCounter;
        public ServerHub(ClientCounter clientCounter)
        {
            _clientCounter = clientCounter;
        }
        public override Task OnConnectedAsync()
        {
            Clients.Caller.SendAsync("Connected", Context.ConnectionId);
            Subscribe();

            if (_clientCounter.GetCount() == 3)
            {
                Notify();
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            Unsubscribe();
            return base.OnDisconnectedAsync(exception);
        }

        public void Subscribe() => _clientCounter.AddClient(Context.ConnectionId);

        public void Unsubscribe() => _clientCounter?.RemoveClient(Context.ConnectionId);

        public void Notify()
        {
            foreach (var client in _clientCounter.GetClients())
                Clients.Client(client).SendAsync("StartGame");
        }

        public async Task SendMessage(string connectionId, string move)
        {
            await Clients.All.SendAsync("ReceiveMessage", connectionId, move);
        }

        public async Task SendPacManCordinates(string serializedObject)
        {
            await Clients.Others.SendAsync("OponentCordinates", serializedObject);
        }

        public async Task SendApplesIndex(int index)
        {
            await Clients.Others.SendAsync("ApplesIndex", index);
        }

        public async Task SendRottenApplesIndex(int index)
        {
            await Clients.Others.SendAsync("RottenApplesIndex", index);
        }

        public async Task SendCoinsIndex(int index)
        {
            await Clients.Others.SendAsync("CoinsIndex", index);
        }

        public async Task SendCherriesIndex(int index)
        {
            await Clients.Others.SendAsync("CherriesIndex", index);
        }
    }
}

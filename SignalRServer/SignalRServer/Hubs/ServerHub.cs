using Microsoft.AspNetCore.SignalR;

namespace SignalRServer.Hubs
{
    public class ServerHub : Hub
    {
        private ClientCounter _clientCounter;
        public ServerHub(ClientCounter clientCounter)
        {
            _clientCounter = clientCounter;
        }
        public override Task OnConnectedAsync()
        {
            Clients.Caller.SendAsync("Connected", Context.ConnectionId);
            _clientCounter.AddClient();

            if (_clientCounter.GetCount() == 3)
            {
                Clients.All.SendAsync("StartGame");
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            _clientCounter?.RemoveClient();
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string connectionId, string move)
        {
            await Clients.All.SendAsync("ReceiveMessage", connectionId, move);
            //await Clients.Others.SendAsync("ReceiveMessage", user, message);
            //await Clients.Caller.SendAsync("ReceiveMessage", user, "delivered: " + message);
        }

        public async Task SendPacManCordinates(string serializedObject)
        {
            await Clients.Others.SendAsync("OponentCordinates", serializedObject);
        }
    }
}

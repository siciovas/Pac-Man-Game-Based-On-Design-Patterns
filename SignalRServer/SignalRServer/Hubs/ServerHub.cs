using ClassLibrary.Commands;
using ClassLibrary.Observer;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace SignalRServer.Hubs
{
    public class ServerHub : Hub, ISubject
    {
        private ClientCounter _clientCounter = ClientCounter.Instance;
        public ServerHub()
        {
        }
        public override Task OnConnectedAsync()
        {
            Clients.Caller.SendAsync("Connected", Context.ConnectionId);
            Subscribe();

            if (_clientCounter.GetCount() == 2)
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

        public async Task Send(string ser)
        {
            ICommand command = JsonSerializer.Deserialize<ICommand>(ser);
            await Clients.Others.SendAsync("CoinsIndex", command);
            switch (command)
            {
                case ApplesIndexCommand _:
                    await Clients.Others.SendAsync("ApplesIndex", command);
                    break;
                case RottenApplesIndexCommand _:
                    await Clients.Others.SendAsync("RottenApplesIndex", command);
                    break;
                case CoinsIndexCommand _:
                    await Clients.Others.SendAsync("CoinsIndex", command);
                    break;
                case CherriesIndexCommand _:
                    await Clients.Others.SendAsync("CherriesIndex", command);
                    break;
            }
        }

        public async Task SendPacManCoordinates(string serializedObject)
        {
            await Clients.Others.SendAsync("OpponentCoordinates", serializedObject);
        }

        public async Task SendApplesIndex(ApplesIndexCommand command)
        {
            await Clients.Others.SendAsync("ApplesIndex", command);
        }

        public async Task SendRottenApplesIndex(RottenApplesIndexCommand command)
        {
            await Clients.Others.SendAsync("RottenApplesIndex", command);
        }

        public async Task SendCoinsIndex(CoinsIndexCommand command)
        {
            await Clients.Others.SendAsync("CoinsIndex", command);
        }

        public async Task SendCherriesIndex(CherriesIndexCommand command)
        {
            await Clients.Others.SendAsync("CherriesIndex", command);
        }
    }
}

using ClassLibrary.Mobs;
using ClassLibrary.Observer;
using Microsoft.AspNetCore.SignalR;

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
            //foreach (var client in _clientCounter.GetClients())
            Clients.All.SendAsync("StartGame");
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
        public async Task GivePointsToOpponent(int score)
        {
            await Clients.Others.SendAsync("OpponentScore", score);
        }
        public async Task PacmanDamage(int damage)
        {
            await Clients.Others.SendAsync("PacmanDamage", damage);
        }
        public async Task Move(string pos)
        {
            await Clients.All.SendAsync("Move", pos);
        }
        public async Task LevelUp(int level)
        {
            await Clients.All.SendAsync("LevelUp", level);
        }
    }
}

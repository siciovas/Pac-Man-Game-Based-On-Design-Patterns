using ClassLibrary.Mobs;
using ClassLibrary.Commands;
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

        public async Task SendCommand(string command, string name)
        {
            switch (name)
            {
                case "RemoveAppleAtIndex":
                    await Clients.Others.SendAsync("RemoveAppleAtIndex", command);
                    break;
                case "RemoveRottenAppleAtIndex":
                    await Clients.Others.SendAsync("RemoveRottenAppleAtIndex", command);
                    break;
                case "RemoveCoinAtIndex":
                    await Clients.Others.SendAsync("RemoveCoinAtIndex", command);
                    break;
                case "RemoveCherryAtIndex":
                    await Clients.Others.SendAsync("RemoveCherryAtIndex", command);
                    break;
                case "GivePointsToOpponent":
                    await Clients.Others.SendAsync("OpponentScore", command);
                    break;
                case "Move":
                    await Clients.All.SendAsync("Move", command);
                    break;
                case "MoveObstacle":
                    await Clients.All.SendAsync("MoveObstacle", command);
                    break;
                case "ChangeSpeedLabel":
                    await Clients.Others.SendAsync("ChangeSpeed", command);
                    break;
            }
        }
        public async Task PacmanDamage(int damage)
        {
            await Clients.Others.SendAsync("PacmanDamage", damage);
        }
        public async Task LevelUp(int level)
        {
            await Clients.All.SendAsync("LevelUp", level);
        }

        public async Task SendRemoveStrawberryAtIndex(RemoveStrawberryAtIndexCommand command)
        {
            await Clients.Others.SendAsync("RemoveStrawberryAtIndex", command);
        }
        public async Task SendAddApple(AddAppleCommand command)
        {
            await Clients.Others.SendAsync("SendAddApple", command);
        }
        public async Task SendAddRottenApple(AddRottenAppleCommand command)
        {
            await Clients.Others.SendAsync("SendAddRottenApple", command);
        }
        public async Task SendAddCherry(AddCherryCommand command)
        {
            await Clients.Others.SendAsync("SendAddCherry", command);
        }
        public async Task SendAddStrawberry(AddStrawberyCommand command)
        {
            await Clients.Others.SendAsync("SendAddStrawberry", command);
        }
        public async Task SendMakeVisitWallCommand(string command)
        {
            await Clients.Others.SendAsync("VisitWall", command);
        }
        public async Task SendMakeVisitSpikeCommand(string command)
        {
            await Clients.Others.SendAsync("VisitSpike", command);
        }
        public async Task SendMakeVisitMobCommand(string command)
        {
            await Clients.Others.SendAsync("VisitMob", command);
        }
    }
}

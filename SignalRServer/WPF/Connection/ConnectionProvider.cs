using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using WPF.Game.ViewModels;
using ClassLibrary.Stores;
using ClassLibrary.Observer;

namespace WPF.Connection
{
    public class ConnectionProvider : IConnectionProvider, IObserver
    {
        private HubConnection _connection;
        private NavigationStore _navigationStore;

        public ConnectionProvider(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7169/serverhub")
                .Build();
            _connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await _connection.StartAsync();
            };
            OnStartGame();
        }

        public void OnStartGame()
        {
            _connection.On("StartGame",
                () =>
                {
                    _navigationStore.CurrentViewModel = new FirstLevelViewModel(this);
                });
        }

        public HubConnection GetConnection()
        {
            return _connection;
        }

        public async void Connect()
        {
            try
            {
                await _connection.StartAsync();
            }
            catch (Exception ex)
            {
            }
        }
    }
}

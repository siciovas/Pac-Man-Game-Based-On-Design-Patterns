using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace WPF.Connection
{
    public class ConnectionProvider : IConnectionProvider
    {
        private HubConnection _connection;

        public ConnectionProvider()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7169/serverhub")
                .Build();
            _connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await _connection.StartAsync();
            };
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

using ClassLibrary.Adapter.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Adapter
{
    public class ConnectionAdapter : IConnectionAdapter
    {
        HubConnection _hubConnection;

        public ConnectionAdapter(HubConnection hubConnection)
        {
            _hubConnection = hubConnection;
        }

        public async Task Invoke(string coordinates, object obj)
        {
            await _hubConnection.InvokeAsync(coordinates, obj);
        }

        public void On<T>(string name, Action action)
        {
            _hubConnection.On(name, action);
        }
    }
}

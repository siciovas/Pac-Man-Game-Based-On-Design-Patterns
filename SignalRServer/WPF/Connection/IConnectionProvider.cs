using Microsoft.AspNetCore.SignalR.Client;

namespace WPF.Connection
{
    public interface IConnectionProvider
    {
        void Connect();
        HubConnection GetConnection();
    }
}

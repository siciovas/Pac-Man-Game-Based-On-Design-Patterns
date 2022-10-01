using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Connection
{
    public interface IConnectionProvider
    {
        void Connect();
        HubConnection GetConnection();
    }
}

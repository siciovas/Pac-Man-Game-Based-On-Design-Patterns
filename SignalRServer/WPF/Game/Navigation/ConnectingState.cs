using ClassLibrary.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Connection;
using WPF.Game.Stores;
using WPF.Game.ViewModels;

namespace WPF.Game.Navigation
{
    public class ConnectingState : State
    {
        private IConnectionProvider _connection;
        private LevelsFacade _context;

        public ConnectingState(IConnectionProvider connection, LevelsFacade levelsFacade)
        {
            _connection = connection;
            _context = levelsFacade;
        }

        public override void GoToView()
        {
            _context.CurrentViewModel = new StartPageViewModel(_connection);
        }
    }
}

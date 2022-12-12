using ClassLibrary.Commands;
using ClassLibrary.Observer;
using ClassLibrary.Views;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using WPF.Connection;
using WPF.Game.Navigation;
using WPF.Game.ViewModels;

namespace WPF.Game.Stores
{
    public class LevelsFacade : IObserver
    {
        private LevelViewModelBase _currentViewModel;
        private State _state;
        private HubConnection _connection;
        private IConnectionProvider _connectionProvider;
        private bool _gameFinished;

        public LevelsFacade(IConnectionProvider connection)
        {
            _connectionProvider = connection;
            _state = new ConnectingState(connection, this);
            _state.GoToView();
            _connection = connection.GetConnection();
            _connection.On<int>("LevelUp", (Level) =>
            {
                LevelUp(Level);
            });
            ListenServer();
            OnStartGame();
        }

        public void TransitionTo(State state)
        {
            Console.WriteLine($"Context: Transition to {state.GetType().Name}.");
            this._state = state;
            this._state.SetContext(this);
            _state.GoToView();
        }

        private GameFinishedViewModel _gameFinishedViewModel;

        public LevelViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel?.Dispose();
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        public event Action CurrentViewModelChanged;

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }

        private void ListenServer()
        {
            _connection.On<string>("OponentCordinates", (serializedObject) =>
            {
                _currentViewModel.SendOponmentCoordinates(serializedObject);
            });

            _connection.On<RemoveAppleAtIndexCommand>("RemoveAppleAtIndex", (command) =>
            {
                _currentViewModel.RemoveApple(command);
            });

            _connection.On<RemoveRottenAppleAtIndexCommand>("RemoveRottenAppleAtIndex", (command) =>
            {
                _currentViewModel.RottenApple(command);
            });

            _connection.On<RemoveCoinAtIndexCommand>("RemoveCoinAtIndex", async (command) =>
            {
                await _currentViewModel.RemoveCoin(command);
            });

            _connection.On<RemoveCherryAtIndexCommand>("RemoveCherryAtIndex", (command) =>
            {
                _currentViewModel.RemoveCherry(command);
            });
            _connection.On<GivePointsToOpponentCommand>("OpponentScore", (command) =>
            {
                _currentViewModel.UpdateOpScore(command);
            });
            _connection.On<string>("Move", (pos) =>
            {
                _currentViewModel.Move(pos);
            });
            _connection.On<int>("PacmanDamage", (damage) =>
            {
                _currentViewModel.DamagePacman(damage);
            });
            _connection.On<string>("MoveObstacle", (serializedObject) =>
            {
                _currentViewModel.MoveObstacle(serializedObject);
            });
            _connection.On<RemoveStrawberryAtIndexCommand>("RemoveStrawberryAtIndex", (command) =>
            {
                _currentViewModel.RemoveStrawberry(command);
            });
            _connection.On<string>("ChangeSpeed", (speed) =>
            {
                _currentViewModel.ChangeSpeed(speed);
            });
            _connection.On<AddAppleCommand>("SendAddApple", (command) =>
            {
                _currentViewModel.AddApple(command);
            });
            _connection.On<AddRottenAppleCommand>("SendAddRottenApple", (command) =>
            {
                _currentViewModel.AddRottenApple(command);
            });
            _connection.On<AddCherryCommand>("SendAddCherry", (command) =>
            {
                _currentViewModel.AddCherry(command);
            });
            _connection.On<AddStrawberyCommand>("SendAddStrawberry", (command) =>
            {
                _currentViewModel.AddStrawberry(command);
            });
        }

        /// <summary>
        /// testing method not used in real game 
        /// </summary>
        public void ChangeLevel()
        {
            if(_state.GetType() != typeof(PlayingState))
            {
                _state = new PlayingState(_connectionProvider, this);
            }
            _state.GoToView();
        }

        public void LevelUp(int Level)
        {
            _state.GoToView();        
        }

        public void OnStartGame()
        {
            _connection.On("StartGame",
                () =>
                {
                    _state = new PlayingState(_connectionProvider, this);
                    _state.GoToView();
                });
        }
    }
}

using ClassLibrary.Commands;
using ClassLibrary.Observer;
using ClassLibrary.Views;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using WPF.Connection;
using WPF.Game.ViewModels;

namespace WPF.Game.Stores
{
    public class LevelsFacade : IObserver
    {
        private LevelViewModelBase _currentViewModel;
        public FirstLevelViewModel _firstLevelViewModel;
        public LevelViewModelBase _secondLevelViewModel;
        public LevelViewModelBase _thirdLevelViewModel;
        public LevelViewModelBase _fourthLevelViewModel;
        public LevelViewModelBase _fifthLevelViewModel;
        private StartPageViewModel _startPageViewModel;
        private HubConnection _connection;
        private IConnectionProvider _connectionProvider;

        public LevelsFacade(IConnectionProvider connection)
        {
            _connectionProvider = connection;
            _startPageViewModel = new StartPageViewModel(connection);
            CurrentViewModel = _startPageViewModel;
            _connection = connection.GetConnection();
            _connection.On<int>("LevelUp", (Level) =>
            {
                LevelUp(Level);
            });
            ListenServer();
            OnStartGame();
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
        }

        /// <summary>
        /// testing method not used in real game 
        /// </summary>
        public void ChangeLevel()
        {
            if (this.CurrentViewModel.Equals(_startPageViewModel))
            {
                _firstLevelViewModel = new FirstLevelViewModel(_connectionProvider);
                this.CurrentViewModel = _firstLevelViewModel;
            }
            else if (this.CurrentViewModel.Equals(_firstLevelViewModel))
            {
                _secondLevelViewModel = new SecondLevelViewModel(_connectionProvider, 20,20);
                this.CurrentViewModel = _secondLevelViewModel;
            }
            else if (this.CurrentViewModel.Equals(_secondLevelViewModel))
            {
                _thirdLevelViewModel = new ThirdLevelViewModel(_connectionProvider,20,20);
                this.CurrentViewModel = _thirdLevelViewModel;
            }
            else if (this.CurrentViewModel.Equals(_thirdLevelViewModel))
            {
                _fourthLevelViewModel = new FourthLevelViewModel(_connectionProvider,20,20);
                this.CurrentViewModel = _fourthLevelViewModel;
            }
            else if (this.CurrentViewModel.Equals(_fourthLevelViewModel))
            {
                _fifthLevelViewModel = new FifthLevelViewModel(_connectionProvider,20,20);
                this.CurrentViewModel = _fifthLevelViewModel;
            }
            else if (this.CurrentViewModel.Equals(_fifthLevelViewModel))
            {
                _gameFinishedViewModel = new GameFinishedViewModel(450,685);
                this.CurrentViewModel = _gameFinishedViewModel;
            }
        }

        public void LevelUp(int Level)
        {
            if (Level == 2) 
            {
                _secondLevelViewModel = new SecondLevelViewModel(_connectionProvider, _firstLevelViewModel.score, _firstLevelViewModel.opponentScore);
                this.CurrentViewModel = _secondLevelViewModel;
            }
            if (Level == 3)
            {
                _thirdLevelViewModel = new ThirdLevelViewModel(_connectionProvider, _secondLevelViewModel.score, _secondLevelViewModel.opponentScore);
                this.CurrentViewModel = _thirdLevelViewModel;
            }
            if (Level == 4)
            {
                _fourthLevelViewModel = new FourthLevelViewModel(_connectionProvider, _thirdLevelViewModel.score, _thirdLevelViewModel.opponentScore);
                this.CurrentViewModel = _fourthLevelViewModel;
            }
            if (Level == 5)
            {
                _fifthLevelViewModel = new FifthLevelViewModel(_connectionProvider, _fourthLevelViewModel.score, _fourthLevelViewModel.opponentScore);
                this.CurrentViewModel = _fifthLevelViewModel;
            }
            if (Level == -1)
            {
                _gameFinishedViewModel = new GameFinishedViewModel(_fifthLevelViewModel.score, _fifthLevelViewModel.opponentScore);
                this.CurrentViewModel = _gameFinishedViewModel;
            }
        }

        public void OnStartGame()
        {
            _connection.On("StartGame",
                () =>
                {
                    _firstLevelViewModel = new FirstLevelViewModel(_connectionProvider);
                    CurrentViewModel = _firstLevelViewModel;
                });
        }
    }
}

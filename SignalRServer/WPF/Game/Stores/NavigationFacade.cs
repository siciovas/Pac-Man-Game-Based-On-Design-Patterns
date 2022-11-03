using ClassLibrary.Observer;
using ClassLibrary.Views;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using WPF.Connection;
using WPF.Game.ViewModels;

namespace WPF.Game.Stores
{
    public class NavigationFacade : IObserver
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

        public NavigationFacade(IConnectionProvider connection)
        {
            _connectionProvider = connection;
            _startPageViewModel = new StartPageViewModel(connection);
            CurrentViewModel = _startPageViewModel;
            _connection = connection.GetConnection();
            _connection.On<int>("LevelUp", (Level) =>
            {
                LevelUp(Level);
            });

            OnStartGame();
        }

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
                _secondLevelViewModel = new SecondLevelViewModel(_connectionProvider);
                this.CurrentViewModel = _secondLevelViewModel;
            }
            else if (this.CurrentViewModel.Equals(_secondLevelViewModel))
            {
                _thirdLevelViewModel = new ThirdLevelViewModel(_connectionProvider);
                this.CurrentViewModel = _thirdLevelViewModel;
            }
            else if (this.CurrentViewModel.Equals(_thirdLevelViewModel))
            {
                _fourthLevelViewModel = new FourthLevelViewModel(_connectionProvider);
                this.CurrentViewModel = _fourthLevelViewModel;
            }
            else if (this.CurrentViewModel.Equals(_fourthLevelViewModel))
            {
                _fifthLevelViewModel = new FifthLevelViewModel(_connectionProvider);
                this.CurrentViewModel = _fifthLevelViewModel;
            }
        }

        public void LevelUp(int Level)
        {
            if (Level == 2) 
            { 
                _secondLevelViewModel = new SecondLevelViewModel(_connectionProvider);
                this.CurrentViewModel = _secondLevelViewModel;
            }
            if (Level == 3)
            {
                _thirdLevelViewModel = new ThirdLevelViewModel(_connectionProvider);
                this.CurrentViewModel = _thirdLevelViewModel;
            }
            if (Level == 4)
            {
                _fourthLevelViewModel = new FourthLevelViewModel(_connectionProvider);
                this.CurrentViewModel = _fourthLevelViewModel;
            }
            if (Level == 5)
            {
                _fifthLevelViewModel = new FifthLevelViewModel(_connectionProvider);
                this.CurrentViewModel = _fifthLevelViewModel;
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

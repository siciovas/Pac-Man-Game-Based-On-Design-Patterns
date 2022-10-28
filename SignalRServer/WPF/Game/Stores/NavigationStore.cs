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

        public NavigationFacade(IConnectionProvider connection)
        {
            _startPageViewModel = new StartPageViewModel(connection);
            CurrentViewModel = _startPageViewModel;
            _firstLevelViewModel = new FirstLevelViewModel(connection);
            _secondLevelViewModel = new SecondLevelViewModel(connection);
            _thirdLevelViewModel = new ThirdLevelViewModel(connection);
            _fourthLevelViewModel = new FourthLevelViewModel(connection);
            _fifthLevelViewModel = new FifthLevelViewModel(connection);
            _connection = connection.GetConnection();

            _firstLevelViewModel.LevelPassed += ChangeLevel;
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

        public void ChangeLevel()
        {
            if (this.CurrentViewModel.Equals(_startPageViewModel))
            {
                this.CurrentViewModel = _firstLevelViewModel;
            }
            else if (this.CurrentViewModel.Equals(_firstLevelViewModel))
            {
                this.CurrentViewModel = _secondLevelViewModel;
            }
            else if (this.CurrentViewModel.Equals(_secondLevelViewModel))
            {
                this.CurrentViewModel = _thirdLevelViewModel;
            }
            else if (this.CurrentViewModel.Equals(_thirdLevelViewModel))
            {
                this.CurrentViewModel = _fourthLevelViewModel;
            }
            else if (this.CurrentViewModel.Equals(_fourthLevelViewModel))
            {
                this.CurrentViewModel = _fifthLevelViewModel;
            }
        }

        public void OnStartGame()
        {
            _connection.On("StartGame",
                () =>
                {
                    CurrentViewModel = _firstLevelViewModel;
                });
        }
    }
}

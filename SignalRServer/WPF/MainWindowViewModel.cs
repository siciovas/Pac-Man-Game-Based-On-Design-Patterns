using Prism.Commands;
using System.Windows.Input;
using WPF.Connection;
using WPF.Game.ViewModels;
using System;
using GalaSoft.MvvmLight.Command;
using ClassLibrary.Views;
using ClassLibrary.Stores;

namespace WPF
{
    public class MainWindowViewModel : ViewModelBase
    {
        IConnectionProvider connection;
        private readonly NavigationStore _navigationStore;
        public ICommand NextView { get; set; }

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
        
        public ViewModelBase _firstLevelViewModel;
        public ViewModelBase _secondLevelViewModel;
        public ViewModelBase _thirdLevelViewModel;
        public ViewModelBase _fourthLevelViewModel;
        public ViewModelBase _fifthLevelViewModel;
        private StartPageViewModel _startPageViewModel;
        public MainWindowViewModel(IConnectionProvider connectionProvider, NavigationStore navigationStore)
        {
            connection = connectionProvider;
            _firstLevelViewModel = new FirstLevelViewModel(connection);
            _secondLevelViewModel = new SecondLevelViewModel(connection);
            _thirdLevelViewModel = new ThirdLevelViewModel(connection);
            _fourthLevelViewModel = new FourthLevelViewModel(connection);
            _fifthLevelViewModel = new FifthLevelViewModel(connection);

            _startPageViewModel = new StartPageViewModel(connectionProvider);
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModel = _startPageViewModel;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            NextView = new RelayCommand(new Action(GoToNextView));
        }

        private void GoToNextView()
        {
            //this button is just for testing do not use it for game purposes bc it only works for one view >:)
            if (_navigationStore.CurrentViewModel.Equals(_startPageViewModel))
            {
                _navigationStore.CurrentViewModel = _firstLevelViewModel;
            }
            else if (_navigationStore.CurrentViewModel.Equals(_firstLevelViewModel))
            {
                _navigationStore.CurrentViewModel = _secondLevelViewModel;
            }
            else if (_navigationStore.CurrentViewModel.Equals(_secondLevelViewModel))
            {
                _navigationStore.CurrentViewModel = _thirdLevelViewModel;
            }
            else if (_navigationStore.CurrentViewModel.Equals(_thirdLevelViewModel))
            {
                _navigationStore.CurrentViewModel = _fourthLevelViewModel;
            }
            else if (_navigationStore.CurrentViewModel.Equals(_fourthLevelViewModel))
            {
                _navigationStore.CurrentViewModel = _fifthLevelViewModel;
            }
        }

        private ICommand _upCommand;
        public ICommand UpCommand
        {
            get
            {
                _upCommand = new DelegateCommand(UpClicked);
                return _upCommand;
            }
        }

        private void UpClicked()
        {
            CurrentViewModel.OnUpClick();
        }

        private ICommand _downCommand;
        public ICommand DownCommand
        {
            get
            {
                _downCommand = new DelegateCommand(DownClicked);
                return _downCommand;
            }
        }

        private void DownClicked()
        {
            CurrentViewModel.OnDownClick();
        }

        private ICommand _leftCommand;
        public ICommand LeftCommand
        {
            get
            {
                _leftCommand = new DelegateCommand(LeftClicked);
                return _leftCommand;
            }
        }

        private void LeftClicked()
        {
            CurrentViewModel.OnLeftClick();
        }
        private ICommand _rightCommand;
        public ICommand RightCommand
        {
            get
            {
                _rightCommand = new DelegateCommand(RightClicked);
                return _rightCommand;
            }
        }

        public void RightClicked()
        {
            CurrentViewModel.OnRightClick();
        }

        /*  public FirstLevelViewModel(IConnectionProvider connectionProvider)
          {
              _coinFactory = new CoinFactory();
              _connection = connectionProvider.GetConnection();
              // UpCommand = new RelayCommand(new Action(UpClicked));
              DownCommand = new RelayCommand(new Action(UpClicked));

              LeftCommand = new RelayCommand(new Action(UpClicked));

              RightCommand = new RelayCommand(new Action(UpClicked));

          }*/

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        public override void OnRightClick()
        {
            return;
        }

        public override void OnDownClick()
        {
            return;
        }

        public override void OnUpClick()
        {
            return;
        }

        public override void OnLeftClick()
        {
            return;
        }
    }
}

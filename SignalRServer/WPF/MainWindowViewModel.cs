using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF.Connection;
using WPF.Levels;
using WPF.Stores;
using WPF.Views;

namespace WPF
{
    public class MainWindowViewModel : ViewModelBase
    {
        IConnectionProvider connection;
        private readonly NavigationStore _navigationStore;

        /*        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel; //sita atkomentuot reik
        */
        public ViewModelBase CurrentViewModel => _firstLecelViewModel;
        public ViewModelBase _firstLecelViewModel;
        public MainWindowViewModel(IConnectionProvider connectionProvider, NavigationStore navigationStore)
        {
            connection = connectionProvider;
            _firstLecelViewModel = new FirstLevelViewModel(connection);
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModel = new StartPageViewModel(connectionProvider);
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
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

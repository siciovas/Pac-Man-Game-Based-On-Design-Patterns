using ClassLibrary.Views;
using GalaSoft.MvvmLight.Command;
using Prism.Commands;
using System;
using System.Windows.Input;
using WPF.Connection;
using WPF.Game.Stores;
using WPF.Game.ViewModels;

namespace WPF
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly LevelsFacade _navigationStore;
        public ChangeButtonViewModel ChangeButton { get; set; }
        public LevelViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        public MainWindowViewModel(LevelsFacade navigationStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            ChangeButton = new ChangeButtonViewModel(navigationStore);
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

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}

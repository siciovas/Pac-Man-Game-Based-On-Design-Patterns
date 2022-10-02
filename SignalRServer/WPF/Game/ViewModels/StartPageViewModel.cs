using GalaSoft.MvvmLight.Command;
using System;
using System.Windows;
using System.Windows.Input;
using WPF.Connection;
using WPF.Views;

namespace WPF.Game.ViewModels
{
    public class StartPageViewModel : ViewModelBase
    {
        IConnectionProvider _connection;
        public ICommand ButtonCommand { get; set; }

        private Visibility _myUserControl1Visibility = Visibility.Collapsed;
        public Visibility IsTextVisible
        {
            get
            {
                return _myUserControl1Visibility;
            }
            private set
            {
                if (value != _myUserControl1Visibility)
                {
                    _myUserControl1Visibility = value;
                    OnPropertyChanged("IsTextVisible");
                }
            }
        }

        public StartPageViewModel(IConnectionProvider connectionProvider)
        {
            _connection = connectionProvider;
            ButtonCommand = new RelayCommand(new Action(MainButtonClick));
        }
        private void MainButtonClick()
        {
            _connection.Connect();
            IsTextVisible = Visibility.Visible;
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

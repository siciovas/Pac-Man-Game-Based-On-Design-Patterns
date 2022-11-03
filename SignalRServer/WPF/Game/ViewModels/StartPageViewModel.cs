using ClassLibrary.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF.Connection;

namespace WPF.Game.ViewModels
{
    public class StartPageViewModel : LevelViewModelBase
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

        public override int score { get => 1; set
            {

            }
        }
        public override int opponentScore { get => 1; set { } }

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

        public override void SendOponmentCoordinates(string serializedObject)
        {
            throw new NotImplementedException();
        }

        public override void RemoveApple(ClassLibrary.Commands.RemoveAppleAtIndexCommand command)
        {
            throw new NotImplementedException();
        }

        public override void RottenApple(ClassLibrary.Commands.RemoveRottenAppleAtIndexCommand command)
        {
            throw new NotImplementedException();
        }

        public override Task RemoveCoin(ClassLibrary.Commands.RemoveCoinAtIndexCommand command)
        {
            throw new NotImplementedException();
        }

        public override void RemoveCherry(ClassLibrary.Commands.RemoveCherryAtIndexCommand command)
        {
            throw new NotImplementedException();
        }

        public override void UpdateOpScore(ClassLibrary.Commands.GivePointsToOpponentCommand command)
        {
            throw new NotImplementedException();
        }

        public override void Move(string pos)
        {
            throw new NotImplementedException();
        }

        public override void DamagePacman(int damage)
        {
            throw new NotImplementedException();
        }
    }
}

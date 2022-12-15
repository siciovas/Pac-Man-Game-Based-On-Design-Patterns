using ClassLibrary.Commands;
using ClassLibrary.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF.Connection;
using ICommand = System.Windows.Input.ICommand;

namespace WPF.Game.ViewModels
{
    public class ButtonClickHandler : IButtonClickHandler
    {
        private readonly IConnectionProvider _connection;

        public ButtonClickHandler(IConnectionProvider connection)
        {
            _connection = connection;
        }

        public void HandleMainButtonClick(string input, ref Visibility IsTextVisible, ref Visibility IsUserAllowed)
        {
            _connection.Connect();
            IsTextVisible = Visibility.Visible;
            IsUserAllowed = Visibility.Collapsed;
        }
    }

    public class ButtonClickHandlerProxy : IButtonClickHandler
    {
        private readonly List<string> blackList = new List<string> { null, "", "NotAllowed", "Rokas" };
        private readonly ButtonClickHandler realButtonClickHandler;

        public ButtonClickHandlerProxy(IConnectionProvider connection)
        {
            realButtonClickHandler = new ButtonClickHandler(connection);
        }

        public void HandleMainButtonClick(string input, ref Visibility IsTextVisible, ref Visibility IsUserAllowed)
        {
            if (CheckAccess(input))
            {
                realButtonClickHandler.HandleMainButtonClick(input, ref IsTextVisible, ref IsUserAllowed);
            }
            else
            {
                IsTextVisible = Visibility.Collapsed;
                IsUserAllowed = Visibility.Visible;
            }
        }

        public bool CheckAccess(string input)
        {
            return !blackList.Contains(input);
        }
    }

    public interface IButtonClickHandler
    {
        void HandleMainButtonClick(string input, ref Visibility textBlock, ref Visibility isUserAllowed);
    }

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
        private Visibility _userNotAllowedText = Visibility.Collapsed;
        public Visibility IsUserAllowed
        {
            get
            {
                return _userNotAllowedText;
            }
            private set
            {
                if (value != _userNotAllowedText)
                {
                    _userNotAllowedText = value;
                    OnPropertyChanged("IsUserAllowed");
                }
            }
        }

        public override int score
        {
            get => 1; set
            {

            }
        }
        public override int opponentScore { get => 1; set { } }

        public string UserName { get; set; }

        public ButtonClickHandlerProxy buttonClickHandler { get; set; }

        public StartPageViewModel(IConnectionProvider connectionProvider)
        {
            _connection = connectionProvider;
            ButtonCommand = new RelayCommand(new Action(MainButtonClick));
            buttonClickHandler = new ButtonClickHandlerProxy(_connection);
        }
        private void MainButtonClick()
        {
            Visibility isTextVisible = IsTextVisible;
            Visibility isUserAllowed = IsUserAllowed;
            buttonClickHandler.HandleMainButtonClick(UserName, ref isTextVisible, ref isUserAllowed);
            IsTextVisible = isTextVisible;
            IsUserAllowed = isUserAllowed;
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

        public override void MoveObstacle(string serializedObject)
        {
            throw new NotImplementedException();
        }

        public override void RemoveStrawberry(ClassLibrary.Commands.RemoveStrawberryAtIndexCommand command)
        {
            throw new NotImplementedException();
        }

        public override void ChangeSpeed(string speed)
        {
            throw new NotImplementedException();
        }

        public override void AddApple(AddAppleCommand command)
        {
            throw new NotImplementedException();
        }

        public override void AddRottenApple(AddRottenAppleCommand command)
        {
            throw new NotImplementedException();
        }

        public override void AddCherry(AddCherryCommand command)
        {
            throw new NotImplementedException();
        }

        public override void AddStrawberry(AddStrawberyCommand command)
        {
            throw new NotImplementedException();
        }

        public override void VisitWall(string command)
        {
            throw new NotImplementedException();
        }

        public override void VisitSpike(string command)
        {
            throw new NotImplementedException();
        }

        public override void VisitMob(string command)
        {
            throw new NotImplementedException();
        }
    }
}

using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;
using WPF.Connection;
using WPF.Game.Factory.Classes;
using WPF.Game.Factory.Interfaces;
using WPF.Game.Singleton.Classes;
using WPF.Views;

namespace WPF.Game.ViewModels
{
    public class FirstLevelViewModel : ViewModelBase
    {
        DispatcherTimer gameTimer = new DispatcherTimer();
        bool goLeft, goRight, goUp, goDown;
        bool noLeft, noRight, noUp, noDown;
        int speed = 8;
        CoinFactory _coinFactory;
        HubConnection _connection;

        private int _yellowPacmanLeft;
        public int YellowPacmanLeft
        {
            get
            {
                return _yellowPacmanLeft;
            }
            private set
            {
                if (value != _yellowPacmanLeft)
                {
                    _yellowPacmanLeft = value;
                    OnPropertyChanged("YellowPacmanLeft");
                }
            }
        }

        private int _yellowPacmanTop;
        public int YellowPacmanTop
        {
            get
            {
                return _yellowPacmanTop;
            }
            private set
            {
                if (value != _yellowPacmanTop)
                {
                    _yellowPacmanTop = value;
                    OnPropertyChanged("YellowPacmanTop");
                }
            }
        }

        private int _greenPacmanLeft;

        public int GreenPacmanLeft
        {
            get
            {
                return _greenPacmanLeft;
            }
            private set
            {
                if (value != _greenPacmanLeft)
                {
                    _greenPacmanLeft = value;
                    OnPropertyChanged("GreenPacmanLeft");
                }
            }
        }

        private int _greenPacmanTop;
        public int GreenPacmanTop
        {
            get
            {
                return _greenPacmanTop;
            }
            private set
            {
                if (value != _greenPacmanTop)
                {
                    _greenPacmanTop = value;
                    OnPropertyChanged("GreenPacmanTop");
                }
            }
        }

        public ObservableCollection<ICoin> Coins { get; set; }

        PacmanHitbox myPacmanHitBox = PacmanHitbox.GetInstance;

        int ghostMoveStep = 130;
        int score = 0;
        int oponentScore = 0;

        public FirstLevelViewModel(IConnectionProvider connectionProvider)
        {
            _coinFactory = new CoinFactory();
            _connection = connectionProvider.GetConnection();

            GreenPacmanTop = 20;
            GreenPacmanLeft = 20;
            YellowPacmanLeft = 20;
            YellowPacmanTop = 20;

            Coins = _coinFactory.GetCoins(1);
            GameSetup();
            ListenServer();
        }

        private void ListenServer()
        {
            _connection.On<int, int>("OponentCordinates", (top, left) =>
            {
                GreenPacmanLeft = left;
                GreenPacmanTop = top;
            });
        }

        private async void GameSetup()
        {
            gameTimer.Tick += GameLoop;
            gameTimer.Interval = TimeSpan.FromMilliseconds(30); ///will tick every 20ms
            gameTimer.Start();
        }

        private async void GameLoop(object? sender, EventArgs e)
        {
            //txtScore.Content = "Score: " + score; TODO bind to score property 
            // show the scoreo to the txtscore label. 

            int AppHeight = (int)Application.Current.MainWindow.Height;
            int AppWidth = (int)Application.Current.MainWindow.Width;
            int oldLeft = YellowPacmanLeft;
            int oldTop = YellowPacmanTop;
            if (goRight)
            {
                YellowPacmanLeft += speed;
            }
            if (goLeft)
            {
                YellowPacmanLeft -= speed;
            }
            if (goUp)
            {
                YellowPacmanTop -= speed;
            }
            if (goDown)
            {
                YellowPacmanTop += speed;
            }

            if (oldLeft != YellowPacmanLeft || oldTop != YellowPacmanTop)
            {
                await _connection.InvokeAsync("SendPacManCordinates", YellowPacmanTop, YellowPacmanLeft);
            }

            if (goDown && YellowPacmanTop + 280 > AppHeight)
            {
                noDown = true;
                goDown = false;
            }
            if (goUp && YellowPacmanTop < 5)
            {
                noUp = true;
                goUp = false;
            }
            if (goLeft && YellowPacmanLeft - 5 < 1)
            {
                noLeft = true;
                goLeft = false;
            }
            if (goRight && YellowPacmanLeft + 40 > AppWidth)
            {
                noRight = true;
                goRight = false;
            }

            Rect pacmanHitBox = myPacmanHitBox.GetCurrentHitboxPosition(YellowPacmanLeft, YellowPacmanTop, 30, 30);

        }

        public override void OnRightClick()
        {
            if (!noRight)
            {
                noLeft = noUp = noDown = false;
                goLeft = goUp = goDown = false;

                goRight = true;
            }
        }

        public override void OnDownClick()
        {
            if (!noDown)
            {
                noUp = noLeft = noRight = false;
                goUp = goLeft = goRight = false;

                goDown = true;
            }
        }

        public override void OnUpClick()
        {
            if (!noUp)
            {
                noRight = noDown = noLeft = false;
                goRight = goDown = goLeft = false;

                goUp = true;
            }
        }

        public override void OnLeftClick()
        {
            if (!noLeft)
            {
                goRight = goUp = goDown = false;
                noRight = noUp = noDown = false;

                goLeft = true;
            }
        }
    }

    /* private void GameOver(string message)
     {
         gameTimer.Stop();
         MessageBox.Show(message, "PacMan Multiplayer");

         System.Diagnostics.Process.Start(Application.ResourceAssembly.Location); //start a new game
         Application.Current.Shutdown(); //shtdown current game 
     }*/
}



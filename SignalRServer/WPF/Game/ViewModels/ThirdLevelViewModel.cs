using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows;
using System.Windows.Threading;
using WPF.Connection;
using WPF.Game.Classes;
using WPF.Game.Factory.Classes;
using WPF.Game.Factory.Interfaces;
using WPF.Game.Singleton.Classes;
using WPF.Views;

namespace WPF.Game.ViewModels
{
    public class ThirdLevelViewModel : ViewModelBase
    {
        DispatcherTimer gameTimer = new DispatcherTimer();
        bool goLeft, goRight, goUp, goDown;
        bool noLeft, noRight, noUp, noDown;
        int speed = 8;
        CoinFactory _coinFactory;
        HubConnection _connection;
        Pacman pacman;
        Pacman greenPacman;

        private int _yellowPacmanLeft;
        public int YellowPacmanLeft
        {
            get
            {
                return pacman.PacmanLeft;
            }
            private set
            {
                if (value != pacman.PacmanLeft)
                {
                    pacman.PacmanLeft = value;
                    OnPropertyChanged("YellowPacmanLeft");
                }
            }
        }

        private int _yellowPacmanTop;
        public int YellowPacmanTop
        {
            get
            {
                return pacman.PacmanTop;
            }
            private set
            {
                if (value != pacman.PacmanTop)
                {
                    pacman.PacmanTop = value;
                    OnPropertyChanged("YellowPacmanTop");
                }
            }
        }

        private int _greenPacmanLeft;

        public int GreenPacmanLeft
        {
            get
            {
                return greenPacman.PacmanLeft;
            }
            private set
            {
                if (value != greenPacman.PacmanLeft)
                {
                    greenPacman.PacmanLeft = value;
                    OnPropertyChanged("GreenPacmanLeft");
                }
            }
        }

        private int _greenPacmanTop;
        public int GreenPacmanTop
        {
            get
            {
                return greenPacman.PacmanTop;
            }
            private set
            {
                if (value != greenPacman.PacmanTop)
                {
                    greenPacman.PacmanTop = value;
                    OnPropertyChanged("GreenPacmanTop");
                }
            }
        }

        public ObservableCollection<ICoin> Coins { get; set; }

        PacmanHitbox myPacmanHitBox = PacmanHitbox.GetInstance;

        int ghostMoveStep = 130;
        int score = 0;
        int oponentScore = 0;

        public ThirdLevelViewModel(IConnectionProvider connectionProvider)
        {
            _coinFactory = new ThirdLevelCoinCreator();
            _connection = connectionProvider.GetConnection();
            pacman = new Pacman();
            greenPacman = new Pacman();
            GreenPacmanTop = 20;
            GreenPacmanLeft = 20;
            YellowPacmanLeft = 20;
            YellowPacmanTop = 20;

            Coins = GetCoins();
            GameSetup();
            ListenServer();
        }

        private ObservableCollection<ICoin> GetCoins()
        {
            ObservableCollection<ICoin> result = new ObservableCollection<ICoin>();
            for (int i = 10; i < 300; i = i + 50)
            {
                for (int j = 50; j < 200; j = j + 50)
                {
                    var coin = _coinFactory.GetCoin(i, j);
                    result.Add(coin);
                }
            }
            for (int i = 10; i < 500; i = i + 50)
            {
                for (int j = 300; j < 550; j = j + 50)
                {
                    var coin = _coinFactory.GetCoin(i, j);
                    result.Add(coin);
                }
            }
            for (int i = 650; i < 700; i = i + 50)
            {
                for (int j = 50; j < 250; j = j + 50)
                {
                    var coin = _coinFactory.GetCoin(i, j);
                    result.Add(coin);
                }
            }

            for (int i = 750; i < 800; i = i + 50)
            {
                for (int j = 50; j < 600; j = j + 50)
                {
                    var coin = _coinFactory.GetCoin(i, j);
                    result.Add(coin);
                }
            }
            return result;
        }

        private void ListenServer()
        {
            _connection.On<string>("OponentCordinates", (serializedObject) =>
            {
                Pacman deserializedObject = JsonSerializer.Deserialize<Pacman>(serializedObject);
                GreenPacmanLeft = deserializedObject.PacmanLeft;
                GreenPacmanTop = deserializedObject.PacmanTop;
            });
        }

        private void GameSetup()
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
                string serializedObject = JsonSerializer.Serialize(pacman);
                await _connection.InvokeAsync("SendPacManCordinates", serializedObject);
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
}

using ClassLibrary.Coins.Factories;
using ClassLibrary.Coins.Interfaces;
using ClassLibrary.Fruits;
using ClassLibrary.Mobs.Interfaces;
using ClassLibrary.Mobs.WeakMob;
using ClassLibrary.Pacmen;
using ClassLibrary.Strategies;
using ClassLibrary.Views;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows;
using System.Windows.Threading;
using WPF.Connection;

namespace WPF.Game.ViewModels
{
    public class FirstLevelViewModel : ViewModelBase
    {
        DispatcherTimer gameTimer = new DispatcherTimer();
        bool goLeft, goRight, goUp, goDown;
        bool noLeft, noRight, noUp, noDown;
        CoinFactory _coinFactory;
        HubConnection _connection;
        WeakMobFactory _mobFactory;
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
        public List<ICoin> CoinsList { get; set; }
        public ObservableCollection<IGhost> Mobs { get; set; }
        public ObservableCollection<Apple> Apples { get; set; }
        public List<Apple> ApplesList { get; set; }
        public ObservableCollection<RottenApple> RottenApples { get; set; }
        public List<RottenApple> RottenApplesList { get; set; }
        public ObservableCollection<Cherry> Cherries { get; set; }
        public List<Cherry> CherriesList { get; set; }
        public ObservableCollection<Strawberry> Strawberries { get; set; }

        PacmanHitbox myPacmanHitBox = PacmanHitbox.GetInstance;

        int ghostMoveStep = 130;
        public int score 
        {
            get
            {
                return pacman.Score;
            }
            private set
            {
                pacman.Score = value;
                OnPropertyChanged("score");
            }
        }
        int oponentScore = 0;

        public FirstLevelViewModel(IConnectionProvider connectionProvider)
        {
            _coinFactory = new BronzeCoinCreator();
            _mobFactory = new WeakMobFactory();
            _connection = connectionProvider.GetConnection();
            pacman = new Pacman();
            greenPacman = new Pacman();
            ApplesList = new List<Apple>();
            var tempApplesList = ApplesList;
            RottenApplesList = new List<RottenApple>();
            var tempRottenApplesList = RottenApplesList;
            CherriesList = new List<Cherry>();
            var tempCherriesList = CherriesList;
            CoinsList = new List<ICoin>();
            var tempCoinsList = CoinsList;
            GreenPacmanTop = 20;
            GreenPacmanLeft = 20;
            YellowPacmanLeft = 20;
            YellowPacmanTop = 20;

            Mobs = SpawnGhosts();
            Coins = Utils.Utils.GetCoins(_coinFactory, ref tempCoinsList);
            CoinsList = tempCoinsList;
            Apples = Utils.Utils.CreateApples(ref tempApplesList);
            ApplesList = tempApplesList;
            RottenApples = Utils.Utils.CreateRottenApples(ref tempRottenApplesList);
            RottenApplesList = tempRottenApplesList;
            Cherries = Utils.Utils.CreateCherries(ref tempCherriesList);
            CherriesList = tempCherriesList;
            Strawberries = Utils.Utils.CreateStrawberries();
            GameSetup();
            ListenServer();
        }

        private ObservableCollection<IGhost> SpawnGhosts()
        {
            ObservableCollection<IGhost> result = new ObservableCollection<IGhost>();
            var firstGhost = _mobFactory.CreateGhost(500, 600);
            var secondGhost = _mobFactory.CreateGhost(50, 750);
            var thirdGhost = _mobFactory.CreateGhost(500, 50);
            var fourthGhost = _mobFactory.CreateGhost(300, 300);
            result.Add(firstGhost);
            result.Add(secondGhost);
            result.Add(thirdGhost);
            result.Add(fourthGhost);
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

            _connection.On<int>("ApplesIndex", (index) =>
            {
                Apples.RemoveAt(index);
                ApplesList.RemoveAt(index);
            });

            _connection.On<int>("RottenApplesIndex", (index) =>
            {
                RottenApples.RemoveAt(index);
                RottenApplesList.RemoveAt(index);
            });

            _connection.On<int>("CoinsIndex", (index) =>
            {
                Coins.RemoveAt(index);
                CoinsList.RemoveAt(index);
            });

            _connection.On<int>("CherriesIndex", (index) =>
            {
                Cherries.RemoveAt(index);
                CherriesList.RemoveAt(index);
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
                YellowPacmanLeft += pacman.Speed;
            }
            if (goLeft)
            {
                YellowPacmanLeft -= pacman.Speed;
            }
            if (goUp)
            {
                YellowPacmanTop -= pacman.Speed;
            }
            if (goDown)
            {
                YellowPacmanTop += pacman.Speed;
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

            foreach (var item in ApplesList)
            {
                Rect hitBox = new Rect(item.Left, item.Top, 30, 30);
                if (pacmanHitBox.IntersectsWith(hitBox))
                {
                    pacman.SetAlgorithm(new GiveSpeed());
                    pacman.Action(ref pacman);
                    var index = ApplesList.FindIndex(a => a.Top == item.Top && a.Left == item.Left);
                    await _connection.InvokeAsync("SendApplesIndex", index);
                    Apples.RemoveAt(index);
                    ApplesList.RemoveAt(index);
                    break;
                }
            }

            foreach (var item in RottenApplesList)
            {
                Rect hitBox = new Rect(item.Left, item.Top, 30, 30);
                if (pacmanHitBox.IntersectsWith(hitBox))
                {
                    pacman.SetAlgorithm(new ReduceSpeed());
                    pacman.Action(ref pacman);
                    var index = RottenApplesList.FindIndex(a => a.Top == item.Top && a.Left == item.Left);
                    await _connection.InvokeAsync("SendRottenApplesIndex", index);
                    RottenApples.RemoveAt(index);
                    RottenApplesList.RemoveAt(index);
                    break;
                }
            }

            foreach (var item in CoinsList)
            {
                Rect hitBox = new Rect(item.Left, item.Top, 10, 10);
                if (pacmanHitBox.IntersectsWith(hitBox))
                {
                    var index = CoinsList.FindIndex(a => a.Top == item.Top && a.Left == item.Left);
                    await _connection.InvokeAsync("SendCoinsIndex", index);
                    Coins.RemoveAt(index);
                    CoinsList.RemoveAt(index);
                    pacman.Score += item.Value;
                    score = pacman.Score;
                    break;
                }
            }

            foreach (var item in CherriesList)
            {
                Rect hitBox = new Rect(item.Left, item.Top, 30, 30);
                if (pacmanHitBox.IntersectsWith(hitBox))
                {
                    pacman.SetAlgorithm(new DoublePoints());
                    pacman.Action(ref pacman);
                    var index = CherriesList.FindIndex(a => a.Top == item.Top && a.Left == item.Left);
                    await _connection.InvokeAsync("SendCherriesIndex", index);
                    Cherries.RemoveAt(index);
                    CherriesList.RemoveAt(index);
                    break;
                }
            }

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



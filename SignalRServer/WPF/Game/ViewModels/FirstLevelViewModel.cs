using ClassLibrary.Coins.Factories;
using ClassLibrary.Coins.Interfaces;
using ClassLibrary.Fruits;
using ClassLibrary.Mobs;
using ClassLibrary.Mobs.WeakMob;
using ClassLibrary._Pacman;
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
using System.Windows.Controls;
using System.Reflection.Metadata;
using System.Windows.Media;
using System.Windows.Data;
using System.Drawing;
using ClassLibrary.MainUnit;
using ClassLibrary.Decorator;

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

        Grid mainGrid;
        Grid opponentGrid;
        public Canvas LayoutRoot { get; private set; }
        public int YellowLeft
        {
            get
            {
                return pacman.Left;
            }
            private set
            {
                if (value != pacman.Left)
                {
                    pacman.Left = value;
                    OnPropertyChanged("YellowLeft");
                }
            }
        }
        public int YellowTop
        {
            get
            {
                return pacman.Top;
            }
            private set
            {
                if (value != pacman.Top)
                {
                    pacman.Top = value;
                    OnPropertyChanged("YellowTop");
                }
            }
        }
        public int GreenLeft
        {
            get
            {
                return greenPacman.Left;
            }
            private set
            {
                if (value != greenPacman.Left)
                {
                    greenPacman.Left = value;
                    OnPropertyChanged("GreenLeft");
                }
            }
        }
        public int GreenTop
        {
            get
            {
                return greenPacman.Top;
            }
            private set
            {
                if (value != greenPacman.Top)
                {
                    greenPacman.Top = value;
                    OnPropertyChanged("GreenTop");
                }
            }
        }
        public ObservableCollection<Coin> Coins { get; set; }
        public List<Coin> CoinsList { get; set; }
        public ObservableCollection<Mob> Mobs { get; set; }
        public ObservableCollection<Apple> Apples { get; set; }
        public List<Apple> ApplesList { get; set; }
        public ObservableCollection<RottenApple> RottenApples { get; set; }
        public List<RottenApple> RottenApplesList { get; set; }
        public ObservableCollection<Cherry> Cherries { get; set; }
        public List<Cherry> CherriesList { get; set; }
        public ObservableCollection<Strawberry> Strawberries { get; set; }

        PacmanHitbox myPacmanHitBox = PacmanHitbox.GetInstance;
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

        public FirstLevelViewModel(IConnectionProvider connectionProvider)
        {
            _coinFactory = new BronzeCoinCreator();
            _mobFactory = new WeakMobFactory();
            _connection = connectionProvider.GetConnection();
            pacman = new Pacman("Pacman");
            greenPacman = new Pacman("PacmanOp");
            LayoutRoot = new Canvas();
            LayoutRoot.Name = "MyCanvas";
            IDecorator grid = new AddLabel(new AddHealthBar(new Pacman("Pacman")));
            mainGrid = grid.Draw();
            opponentGrid = new AddLabel(new AddHealthBar(new Pacman("PacmanOp"))).Draw();
            LayoutRoot.Children.Add(mainGrid);
            LayoutRoot.Children.Add(opponentGrid);
            ApplesList = new List<Apple>();
            var tempApplesList = ApplesList;
            RottenApplesList = new List<RottenApple>();
            var tempRottenApplesList = RottenApplesList;
            CherriesList = new List<Cherry>();
            var tempCherriesList = CherriesList;
            CoinsList = new List<Coin>();
            var tempCoinsList = CoinsList;
            GreenTop = 20;
            GreenLeft = 20;
            YellowLeft = 20;
            YellowTop = 20;

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

        private ObservableCollection<Mob> SpawnGhosts()
        {
            ObservableCollection<Mob> result = new ObservableCollection<Mob>();
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
                GreenLeft = deserializedObject.Left;
                GreenTop = deserializedObject.Top;
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
            Canvas.SetLeft(mainGrid, YellowLeft);
            Canvas.SetTop(mainGrid, YellowTop);
            Canvas.SetLeft(opponentGrid, GreenLeft);
            Canvas.SetTop(opponentGrid, GreenTop);
            //txtScore.Content = "Score: " + score; TODO bind to score property 
            // show the scoreo to the txtscore label. 

            int AppHeight = (int)Application.Current.MainWindow.Height;
            int AppWidth = (int)Application.Current.MainWindow.Width;
            int oldLeft = YellowLeft;
            int oldTop = YellowTop;
            if (goRight)
            {
                YellowLeft += pacman.Speed;
            }
            if (goLeft)
            {
                YellowLeft -= pacman.Speed;
            }
            if (goUp)
            {
                YellowTop -= pacman.Speed;
            }
            if (goDown)
            {
                YellowTop += pacman.Speed;
            }

            if (oldLeft != YellowLeft || oldTop != YellowTop)
            {
                string serializedObject = JsonSerializer.Serialize(new { Top = pacman.Top, Left = pacman.Left });
                await _connection.InvokeAsync("SendPacManCordinates", serializedObject);
            }

            if (goDown && YellowTop + 280 > AppHeight)
            {
                noDown = true;
                goDown = false;
            }
            if (goUp && YellowTop < 5)
            {
                noUp = true;
                goUp = false;
            }
            if (goLeft && YellowLeft - 5 < 1)
            {
                noLeft = true;
                goLeft = false;
            }
            if (goRight && YellowLeft + 40 > AppWidth)
            {
                noRight = true;
                goRight = false;
            }

            Rect pacmanHitBox = myPacmanHitBox.GetCurrentHitboxPosition(YellowLeft, YellowTop, 30, 30);

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
}



using ClassLibrary.Coins.Factories;
using ClassLibrary.Coins.Interfaces;
using ClassLibrary.Fruits;
using ClassLibrary.Mobs;
using ClassLibrary.Mobs.StrongMob;
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
using ClassLibrary.Decorator;
using System.Windows.Controls;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace WPF.Game.ViewModels
{
    public class SecondLevelViewModel : LevelViewModelBase
    {
        DispatcherTimer gameTimer = new DispatcherTimer();
        bool goLeft, goRight, goUp, goDown;
        bool noLeft, noRight, noUp, noDown;
        CoinFactory _BronzeCoinFactory;
        CoinFactory _SilverCoinFactory;
        HubConnection _connection;
        WeakMobFactory _mobFactory;
        StrongMobFactory _strongMobFactory;
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

        public int opponentScore
        {
            get
            {
                return greenPacman.Score;
            }
            private set
            {
                greenPacman.Score = value;
                OnPropertyChanged("opponentScore");
            }
        }
        public SecondLevelViewModel(IConnectionProvider connectionProvider)
        {
            _BronzeCoinFactory = new BronzeCoinCreator();
            _SilverCoinFactory = new SilverCoinCreator();
            _mobFactory = new WeakMobFactory();
            _strongMobFactory = new StrongMobFactory();
            _connection = connectionProvider.GetConnection();
            pacman = new Pacman("Pacman");
            greenPacman = pacman.Copy();
            LayoutRoot = new Canvas();
            LayoutRoot.Name = "MyCanvas";
            IDecorator grid = new AddLabel(new AddHealthBar(pacman, 100));
            mainGrid = grid.Draw();
            opponentGrid = new AddLabel(new AddHealthBar(greenPacman, 100)).Draw();
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

            Coins = Utils.Utils.GetFirstHalfCoins(_BronzeCoinFactory, ref tempCoinsList);
            Coins = Utils.Utils.GetSecondHalfCoins(_SilverCoinFactory, Coins, ref tempCoinsList);
            CoinsList = tempCoinsList;
            Mobs = SpawnGhosts();
            Apples = Utils.Utils.CreateApples(ref tempApplesList);
            ApplesList = tempApplesList;
            RottenApples = Utils.Utils.CreateRottenApples(ref tempRottenApplesList);
            RottenApplesList = tempRottenApplesList;
            Cherries = Utils.Utils.CreateCherries(ref tempCherriesList);
            CherriesList = tempCherriesList;
            Strawberries = Utils.Utils.CreateStrawberries();
            ListenServer();
        }

        private ObservableCollection<Mob> SpawnGhosts()
        {
            ObservableCollection<Mob> result = new ObservableCollection<Mob>();
            var firstGhost = _mobFactory.CreateGhost(500, 600);
            var secondGhost = _strongMobFactory.CreateGhost(50, 750);
            var thirdGhost = _strongMobFactory.CreateGhost(500, 50);
            var fourthGhost = _strongMobFactory.CreateGhost(300, 300);
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
            _connection.On<int>("OpponentScore", (score) =>
            {
                greenPacman.Score = score;
                opponentScore = greenPacman.Score;
            });
            _connection.On<string>("Move", (pos) =>
            {
                var deserializedObject = JsonConvert.DeserializeObject<dynamic>(pos);
                Mobs[(int)deserializedObject.Index].GoLeft = (bool)deserializedObject.GoLeft;
                Mobs[(int)deserializedObject.Index].Left = (int)deserializedObject.Position;
            });
            _connection.On<int>("PacmanDamage", (damage) =>
            {
                IDecorator grid = new AddLabel(new AddHealthBar(greenPacman, damage));
                opponentGrid = grid.Draw();
                LayoutRoot.Children.Remove(LayoutRoot.Children[1]);
                LayoutRoot.Children.Insert(1, opponentGrid);
                Canvas.SetLeft(opponentGrid, GreenLeft);
                Canvas.SetTop(opponentGrid, GreenTop);
            });
            _connection.On<int>("LevelUp", (Level) =>
            {
                if(Level == 2)
                {
                    GameSetup();
                }
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
            Canvas.SetLeft(mainGrid, YellowLeft);
            Canvas.SetTop(mainGrid, YellowTop);
            Canvas.SetLeft(opponentGrid, GreenLeft);
            Canvas.SetTop(opponentGrid, GreenTop);

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
                    await _connection.InvokeAsync("GivePointsToOpponent", score);
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
                    score = pacman.Score;
                    await _connection.InvokeAsync("GivePointsToOpponent", pacman.Score);
                    var index = CherriesList.FindIndex(a => a.Top == item.Top && a.Left == item.Left);
                    await _connection.InvokeAsync("SendCherriesIndex", index);
                    Cherries.RemoveAt(index);
                    CherriesList.RemoveAt(index);
                    break;
                }
            }

            int mobIndex = 0;
            foreach (var item in Mobs)
            {
                Rect hitBox = new Rect(item.Left, item.Top, 30, 30);
                if (pacmanHitBox.IntersectsWith(hitBox))
                {
                    pacman.Health -= item.GetDamage();
                    await _connection.InvokeAsync("PacmanDamage", pacman.Health);
                    IDecorator grid = new AddLabel(new AddHealthBar(pacman, pacman.Health));
                    mainGrid = grid.Draw();
                    LayoutRoot.Children.Remove(LayoutRoot.Children[0]);
                    LayoutRoot.Children.Insert(0, mainGrid);
                    Canvas.SetLeft(mainGrid, YellowLeft);
                    Canvas.SetTop(mainGrid, YellowTop);
                }
                if (_connection.State.HasFlag(HubConnectionState.Connected))
                {
                    if (item.GoLeft && item.Left + 40 > AppWidth)
                    {
                        string a = JsonConvert.SerializeObject(new { Position = item.Left, Index = mobIndex, GoLeft = false });
                        await _connection.InvokeAsync("Move", a);
                    }
                    else if (!item.GoLeft && item.Left - 5 < 1)
                    {
                        string a = JsonConvert.SerializeObject(new { Position = item.Left, Index = mobIndex, GoLeft = true });
                        await _connection.InvokeAsync("Move", a);
                    }
                    if (item.GoLeft)
                    {
                        string a = JsonConvert.SerializeObject(new { Position = item.Left + item.GetSpeed(), Index = mobIndex, GoLeft = true });
                        await _connection.InvokeAsync("Move", a);
                    }
                    else
                    {
                        string a = JsonConvert.SerializeObject(new { Position = item.Left - item.GetSpeed(), Index = mobIndex, GoLeft = false });
                        await _connection.InvokeAsync("Move", a);
                    }
                    mobIndex++;
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

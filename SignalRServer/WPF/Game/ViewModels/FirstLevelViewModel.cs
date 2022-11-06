using ClassLibrary._Pacman;
using ClassLibrary.Coins.Factories;
using ClassLibrary.Coins.Interfaces;
using ClassLibrary.Commands;
using ClassLibrary.Decorator;
using ClassLibrary.Fruits;
using ClassLibrary.Mobs;
using ClassLibrary.Mobs.WeakMob;
using ClassLibrary.Strategies;
using ClassLibrary.Views;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using WPF.Connection;
using static System.Net.Mime.MediaTypeNames;
using Application = System.Windows.Application;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.Windows.Controls;
using System.Reflection.Metadata;
using System.Windows.Media;
using System.Windows.Data;
using System.Drawing;
using ClassLibrary.MainUnit;
using ClassLibrary.Decorator;
using ClassLibrary.Bridge;
using System.Windows.Shapes;
using Rectangle = System.Windows.Shapes.Rectangle;
using System.Linq;
using ClassLibrary.CoinMapping;
using ClassLibrary.Adapter;

namespace WPF.Game.ViewModels
{
    public class FirstLevelViewModel : LevelViewModelBase

    {
        DispatcherTimer gameTimer = new DispatcherTimer();
        bool goLeft, goRight, goUp, goDown;
        bool noLeft, noRight, noUp, noDown;
        CoinFactory _coinFactory;
        HubConnection _connection;
        WeakMobFactory _mobFactory;
        CoinMapProvider _coinMapProvider;
        Pacman pacman;
        Pacman greenPacman;
        private bool _levelPassed;
        Grid mainGrid;
        Grid opponentGrid;
        public event Action LevelPassed;
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
        public ObservableCollection<Mob> Mobs { get; set; }
        public ObservableCollection<Apple> Apples { get; set; }
        public ObservableCollection<RottenApple> RottenApples { get; set; }
        public ObservableCollection<Cherry> Cherries { get; set; }
        public ObservableCollection<Strawberry> Strawberries { get; set; }
        public ObservableCollection<Wall> Walls { get; set; }
        public ObservableCollection<Spike> Spikes { get; set; }


        PacmanHitbox myPacmanHitBox = PacmanHitbox.GetInstance;
        public override int score
        {
            get
            {
                return pacman.Score;
            }
            set
            {
                pacman.Score = value;
                OnPropertyChanged("score");
            }
        }

        public override int opponentScore
        {
            get
            {
                return greenPacman.Score;
            }
            set
            {
                greenPacman.Score = value;
                OnPropertyChanged("opponentScore");
            }
        }

        public FirstLevelViewModel(IConnectionProvider connectionProvider)
        {
            _coinFactory = new BronzeCoinCreator();
            _coinMapProvider = new CoinMapProvider();
            _mobFactory = new WeakMobFactory();
            _connection = connectionProvider.GetConnection();
            pacman = new Pacman("Pacman");
            greenPacman = pacman.Copy();
            LayoutRoot = new Canvas();
            LayoutRoot.Name = "MyCanvas";
            IDecorator grid = new AddLabel(new AddHealthBar(pacman, pacman.Health));
            mainGrid = grid.Draw();
            opponentGrid = new AddLabel(new AddHealthBar(greenPacman, greenPacman.Health)).Draw();
            LayoutRoot.Children.Add(mainGrid);
            LayoutRoot.Children.Add(opponentGrid);
            GreenTop = 20;
            GreenLeft = 20;
            YellowLeft = 20;
            YellowTop = 20;
            _levelPassed = false;

            Mobs = SpawnGhosts();
            Coins = _coinMapProvider.GetCoins(10, 800, 50, 600, _coinFactory);
            Apples = Utils.Utils.CreateApples();
            RottenApples = Utils.Utils.CreateRottenApples();
            Cherries = Utils.Utils.CreateCherries();
            Strawberries = Utils.Utils.CreateStrawberries();
            Walls = CreateWalls();
            Spikes = CreateSpikes();
            GameSetup();
        }

        private ObservableCollection<Wall> CreateWalls()
        {
            ObservableCollection<Wall> wall = new ObservableCollection<Wall>();
            for (int i = 200; i < 500; i += 30)
            {
                var temp = new Wall(new StandardFeature());
                temp.SetDamage();
                temp.Left = 600;
                temp.Top = i;
                wall.Add(temp);
            }
            for (int i = 200; i < 500; i += 30)
            {
                var temp = new Wall(new StandardFeature());
                temp.SetDamage();
                temp.Left = 200;
                temp.Top = i;
                wall.Add(temp);
            }
            return wall;
        }

        private ObservableCollection<Spike> CreateSpikes()
        {
            ObservableCollection<Spike> spikes = new ObservableCollection<Spike>();
            for (int i = 250; i < 450; i += 30)
            {
                var temp = new Spike(new LethalFeature());
                temp.SetDamage();
                temp.Left = i;
                temp.Top = 150;
                spikes.Add(temp);
            }
            
            return spikes;
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

        public override void SendOponmentCoordinates(string serializedObject)
        {
            Pacman deserializedObject = JsonSerializer.Deserialize<Pacman>(serializedObject);
            GreenLeft = deserializedObject.Left;
            GreenTop = deserializedObject.Top;
        }

        public override void RemoveApple(RemoveAppleAtIndexCommand command)
        {
            command.Execute(Apples);
        }

        public override void RottenApple(RemoveRottenAppleAtIndexCommand command)
        {
            command.Execute(RottenApples);

        }

        public override async Task RemoveCoin(RemoveCoinAtIndexCommand command)
        {
            command.Execute(Coins);
            if (Coins.Count == 0)
            {
                gameTimer.Stop();
                _levelPassed = true;
                await _connection.InvokeAsync("LevelUp", 2);
            }
        }

        public override void RemoveCherry(RemoveCherryAtIndexCommand command)
        {
            command.Execute(Cherries);
        }

        public override void UpdateOpScore(GivePointsToOpponentCommand command)
        {
            command.Execute(greenPacman);
            opponentScore = greenPacman.Score;
        }

        public override void Move(string pos)
        {
            var serializedObject = JsonConvert.DeserializeObject<dynamic>(pos);
            Mobs[(int)serializedObject.Index].GoLeft = (bool)serializedObject.GoLeft;
            Mobs[(int)serializedObject.Index].Left = (int)serializedObject.Position;
        }

        public override void DamagePacman(int damage)
        {
            IDecorator grid = new AddLabel(new AddHealthBar(greenPacman, damage));
            opponentGrid = grid.Draw();
            LayoutRoot.Children.Remove(LayoutRoot.Children[1]);
            LayoutRoot.Children.Insert(1, opponentGrid);
            Canvas.SetLeft(opponentGrid, GreenLeft);
            Canvas.SetTop(opponentGrid, GreenTop);
        }

        public override void MoveObstacle(string serializedObject)
        {
            var serObject = JsonConvert.DeserializeObject<dynamic>(serializedObject);
            Spikes[(int)serObject.Index].GoLeft = (bool)serObject.GoLeft;
            Spikes[(int)serObject.Index].Left = (int)serObject.Position;
        }

        public override void RemoveStrawberry(RemoveStrawberryAtIndexCommand command)
        {
            command.Execute(Strawberries);
        }

        private async void GameSetup()
        {
            gameTimer.Tick += GameLoop;
            gameTimer.Interval = TimeSpan.FromMilliseconds(30); ///will tick every 20ms
          
            gameTimer.Start();
        }

        private async void GameLoop(object? sender, EventArgs e)
        {
            if (Coins.Count == 0) 
            {
                return;
            }
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

            if (goDown && YellowTop + 105 > AppHeight)
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
            if (goRight && YellowLeft + 60 > AppWidth)
            {
                noRight = true;
                goRight = false;
            }

            Rect pacmanHitBox = myPacmanHitBox.GetCurrentHitboxPosition(YellowLeft, YellowTop, 30, 30);

            foreach (var item in Apples)
            {
                Rect hitBox = new Rect(item.Left, item.Top, 30, 30);
                if (pacmanHitBox.IntersectsWith(hitBox))
                {
                    pacman.SetAlgorithm(new GiveSpeed());
                    pacman.Action(ref pacman);
                    var index = Apples.IndexOf(Apples.Where(a => a.Top == item.Top && a.Left == item.Left).FirstOrDefault());
                    Apples.RemoveAt(index);
                    await _connection.InvokeAsync("SendRemoveAppleAtIndex", new RemoveAppleAtIndexCommand(index));
                    break;
                }
            }

            foreach (var item in RottenApples)
            {
                Rect hitBox = new Rect(item.Left, item.Top, 30, 30);
                if (pacmanHitBox.IntersectsWith(hitBox))
                {
                    pacman.SetAlgorithm(new ReduceSpeed());
                    pacman.Action(ref pacman);
                    var index = RottenApples.IndexOf(RottenApples.Where(a => a.Top == item.Top && a.Left == item.Left).FirstOrDefault());
                    RottenApples.RemoveAt(index);
                    await _connection.InvokeAsync("SendRemoveRottenAppleAtIndex", new RemoveRottenAppleAtIndexCommand(index));
                    break;
                }
            }

            foreach (var item in Coins)
            {
                Rect hitBox = new Rect(item.Left, item.Top, 10, 10);
                if (pacmanHitBox.IntersectsWith(hitBox))
                {
                    var index = Coins.IndexOf(Coins.Where(a => a.Top == item.Top && a.Left == item.Left).FirstOrDefault());
                    Coins.RemoveAt(index);
                    await _connection.InvokeAsync("SendRemoveCoinAtIndex", new RemoveCoinAtIndexCommand(index));
                    pacman.Score += item.Value;
                    score = pacman.Score;
                    await _connection.InvokeAsync("GivePointsToOpponent", new GivePointsToOpponentCommand(score));

                    break;
                }
            }

            foreach (var item in Cherries)
            {
                Rect hitBox = new Rect(item.Left, item.Top, 30, 30);
                if (pacmanHitBox.IntersectsWith(hitBox))
                {
                    pacman.SetAlgorithm(new DoublePoints());
                    pacman.Action(ref pacman);
                    score = pacman.Score;
                    await _connection.InvokeAsync("GivePointsToOpponent", new GivePointsToOpponentCommand(score));
                    var index = Cherries.IndexOf(Cherries.Where(a => a.Top == item.Top && a.Left == item.Left).FirstOrDefault());
                    Cherries.RemoveAt(index);
                    await _connection.InvokeAsync("SendRemoveCherryAtIndex", new RemoveCherryAtIndexCommand(index));
                    break;
                }
            }
            foreach (var item in Strawberries)
            {
                Rect hitBox = new Rect(item.Left, item.Top, 30, 30);
                if (pacmanHitBox.IntersectsWith(hitBox))
                {
                    pacman.SetAlgorithm(new MakeGhost());
                    pacman.Action(ref pacman);
                    var index = Strawberries.IndexOf(Strawberries.Where(a => a.Top == item.Top && a.Left == item.Left).FirstOrDefault());
                    Strawberries.RemoveAt(index);
                    await _connection.InvokeAsync("SendRemoveStrawberryAtIndex", new RemoveStrawberryAtIndexCommand(index));
                    break;
                }
            }

            foreach (var item in Walls)
            {
                Rect hitBox = new Rect(item.Left, item.Top, 30, 30);
                if (pacmanHitBox.IntersectsWith(hitBox))
                {
                    if (goRight && !pacman.GhostMode)
                    {
                        var damage = item.GetDamage();
                        pacman.Health = pacman.Health - item.GetDamage();
                        YellowLeft = 0;
                    }
                    else if (goLeft && !pacman.GhostMode)
                    {
                        var damage = item.GetDamage();
                        pacman.Health = pacman.Health - item.GetDamage();
                        YellowLeft = 800;
                    }
                    if (goUp && !pacman.GhostMode)
                    {
                        var damage = item.GetDamage();
                        pacman.Health = pacman.Health - item.GetDamage();
                        YellowTop = 600;
                    }
                    if (goDown && !pacman.GhostMode )
                    {
                        var damage = item.GetDamage();
                        pacman.Health = pacman.Health - item.GetDamage();
                        YellowTop = 0;
                    }
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
                    if (pacman.Health < 0)
                    {
                        YellowLeft = 0;
                        YellowTop = 0;
                        pacman.Health = 100;
                        grid = new AddLabel(new AddHealthBar(pacman, 100));
                        mainGrid = grid.Draw();
                        LayoutRoot.Children.Remove(LayoutRoot.Children[0]);
                        LayoutRoot.Children.Insert(0, mainGrid);
                        Canvas.SetLeft(mainGrid, YellowLeft);
                        Canvas.SetTop(mainGrid, YellowTop);
                        await _connection.InvokeAsync("PacmanDamage", pacman.Health);
                    }
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

            int spikeIndex = 0;
            foreach (var item in Spikes)
            {
                Rect hitBox = new Rect(item.Left, item.Top, 30, 30);
                if (pacmanHitBox.IntersectsWith(hitBox))
                {
                    if (!pacman.GhostMode)
                    {
                        pacman.Health = pacman.Health - item.GetDamage();
                        if (pacman.Health < 0)
                        {
                            YellowLeft = 0;
                            YellowTop = 0;
                        }
                    }
                    break;
                }
                if (item.GoLeft && item.Left + 40 > AppWidth)
                {
                    string a = JsonConvert.SerializeObject(new { Position = item.Left, Index = spikeIndex, GoLeft = false });
                    await _connection.InvokeAsync("MoveObstacle", a);
                }
                else if (!item.GoLeft && item.Left - 5 < 1)
                {
                    string a = JsonConvert.SerializeObject(new { Position = item.Left, Index = spikeIndex, GoLeft = true });
                    await _connection.InvokeAsync("MoveObstacle", a);
                }
                if (item.GoLeft)
                {
                    string a = JsonConvert.SerializeObject(new { Position = item.Left + 3, Index = spikeIndex, GoLeft = true });
                    await _connection.InvokeAsync("MoveObstacle", a);
                }
                else
                {
                    string a = JsonConvert.SerializeObject(new { Position = item.Left - 3, Index = spikeIndex, GoLeft = false });
                    await _connection.InvokeAsync("MoveObstacle", a);
                }
                spikeIndex++;
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



using ClassLibrary._Pacman;
using ClassLibrary.Coins.Interfaces;
using ClassLibrary.Commands;
using ClassLibrary.Decorator;
using ClassLibrary.Fruits;
using ClassLibrary.Mobs;
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
using Application = System.Windows.Application;
using JsonSerializer = System.Text.Json.JsonSerializer;
using ClassLibrary.Bridge;
using ClassLibrary.ChainOfResponsibility;
using ClassLibrary.TemplateMethod;
using ClassLibrary.Memento;
using System.Collections.Generic;
using GalaSoft.MvvmLight.CommandWpf;

using ClassLibrary.Interpreter;
using System.Linq.Expressions;
using ClassLibrary.Visitor;
using ClassLibrary;

namespace WPF.Game.ViewModels
{
    public class FirstLevelViewModel : LevelViewModelBase

    {
        DispatcherTimer gameTimer = new DispatcherTimer();
        bool goLeft, goRight, goUp, goDown;
        bool noLeft, noRight, noUp, noDown;
        HubConnection _connection;
        Pacman pacman;
        Pacman greenPacman;
        private bool _levelPassed;
        Grid mainGrid;
        Grid opponentGrid;
        public event Action LevelPassed;
        AbstractHandler handler = new AppleHandler();
        private List<IMemento> _mementos = new List<IMemento>();
        public System.Windows.Input.ICommand SaveState { get; set; }
        public System.Windows.Input.ICommand RestoreLastState { get; set; }

        ClassLibrary.Interpreter.Expression ApplesExpression;
        ClassLibrary.Interpreter.Expression RottenApplesExpression;
        ClassLibrary.Interpreter.Expression CherriesExpression;
        ClassLibrary.Interpreter.Expression StrawberriesExpression;
        List<Apple> copyApples;
        List<RottenApple> copyRottenApples;
        List<Cherry> copyCherries;
        List<Strawberry> copyStrawberries;
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

        private string _name = string.Empty;
        public string CmdName
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("CmdName"); }
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

        private void SavePacmanState()
        {
            _mementos.Add(pacman.Save());
        }

        private void RecoverLastPacmanState()
        {
            if (this._mementos.Count == 0)
            {
                return;
            }

            var memento = this._mementos.Last();
            this._mementos.Remove(memento);

            try
            {
                this.pacman.Restore(memento);
            }
            catch (Exception)
            {
            }
        }
        public FirstLevelViewModel(IConnectionProvider connectionProvider)
        {
            SaveState = new RelayCommand(new Action(SavePacmanState));
            RestoreLastState = new RelayCommand(new Action(RecoverLastPacmanState));

            handler.SetNext(new CherryHandler()).SetNext(new RottenAppleHandler()).SetNext(new StrawberryHandler());
            Coins = new ObservableCollection<Coin>();
            Mobs = new ObservableCollection<Mob>();
            Spikes = new ObservableCollection<Spike>();
            Walls = new ObservableCollection<Wall>();
            _connection = connectionProvider.GetConnection();
            pacman = new Pacman("Pacman");
            greenPacman = pacman.Copy();
            LayoutRoot = new Canvas();
            LayoutRoot.Name = "MyCanvas";
            IDecorator grid = new ShowSpeed(new AddLabel(new AddHealthBar(pacman, pacman.Health)), pacman.Speed.ToString());
            mainGrid = grid.Draw();
            opponentGrid = new ShowSpeed(new AddLabel(new AddHealthBar(greenPacman, greenPacman.Health)), greenPacman.Speed.ToString()).Draw();
            LayoutRoot.Children.Add(mainGrid);
            LayoutRoot.Children.Add(opponentGrid);
            GreenTop = 20;
            GreenLeft = 20;
            YellowLeft = 20;
            YellowTop = 20;
            _levelPassed = false;

            MapLoader mapLoader = new FirstLevelLoader();
            var ApplesCopy = Apples;
            var RottenApplesCopy = RottenApples;
            var CherriesCopy = Cherries;
            var StrawberriesCopy = Strawberries;
            var SpikesCopy = Spikes;
            var WallsCopy = Walls;
            var MobsCopy = Mobs;
            var CoinsCopy = Coins;
            mapLoader.LoadMap(ref ApplesCopy, ref RottenApplesCopy, ref CherriesCopy, ref StrawberriesCopy, ref SpikesCopy, ref MobsCopy, ref CoinsCopy, ref WallsCopy);
            Apples = ApplesCopy;
            RottenApples = RottenApplesCopy;
            Cherries = CherriesCopy;
            Strawberries = StrawberriesCopy;
            Spikes = SpikesCopy;
            Walls = WallsCopy;
            Mobs = MobsCopy;
            Coins = CoinsCopy;
            copyApples = Apples.ToList();
            copyRottenApples = RottenApples.ToList();
            copyCherries = Cherries.ToList();
            copyStrawberries = Strawberries.ToList();
            ApplesExpression = new ApplesRestoreExpression(Apples.ToList());
            RottenApplesExpression = new RottenApplesRestoreExpression(RottenApples.ToList());
            CherriesExpression = new CherriesRestoreExpression(Cherries.ToList());
            StrawberriesExpression = new StrawberriesRestoreExpression(Strawberries.ToList());
            GameSetup();
        }
        public override void SendOponmentCoordinates(string serializedObject)
        {
            Coordinates deserializedObject = JsonSerializer.Deserialize<Coordinates>(serializedObject);
            GreenLeft = deserializedObject.Left;
            GreenTop = deserializedObject.Top;
        }

        public override void RemoveApple(string command)
        {
            RemoveAppleAtIndexCommand _command = JsonSerializer.Deserialize<RemoveAppleAtIndexCommand>(command);
            _command.Execute(Apples);
        }

        public override void RottenApple(string command)
        {
            RemoveRottenAppleAtIndexCommand _command = JsonSerializer.Deserialize<RemoveRottenAppleAtIndexCommand>(command);
            _command.Execute(RottenApples);
        }

        public override async Task RemoveCoin(string command)
        {
            RemoveCoinAtIndexCommand _command = JsonSerializer.Deserialize<RemoveCoinAtIndexCommand>(command);
            _command.Execute(Coins);
            if (Coins.Count == 0)
            {
                gameTimer.Stop();
                _levelPassed = true;
                await _connection.InvokeAsync("LevelUp", 2);
            }
        }

        public override void RemoveCherry(string command)
        {
            RemoveCherryAtIndexCommand _command = JsonSerializer.Deserialize<RemoveCherryAtIndexCommand>(command);
            _command.Execute(Cherries);
        }

        public override void UpdateOpScore(string command)
        {
            GivePointsToOpponentCommand _command = JsonSerializer.Deserialize<GivePointsToOpponentCommand>(command);
            _command.Execute(greenPacman);
            opponentScore = greenPacman.Score;
        }

        public override void Move(string pos)
        {
            MoveModel serializedObject = JsonConvert.DeserializeObject<MoveModel>(pos);
            Mobs[serializedObject.Index].GoLeft = serializedObject.GoLeft;
            Mobs[serializedObject.Index].Left = serializedObject.Position;
        }

        public override void DamagePacman(int damage)
        {
            greenPacman.Health = damage;
            IDecorator grid = new ShowSpeed(new AddLabel(new AddHealthBar(greenPacman, damage)), greenPacman.Speed.ToString());
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
        public override void ChangeSpeed(string speed)
        {
            IDecorator grid = new ShowSpeed(new AddLabel(new AddHealthBar(greenPacman, greenPacman.Health)), speed);
            opponentGrid = grid.Draw();
            LayoutRoot.Children.Remove(LayoutRoot.Children[1]);
            LayoutRoot.Children.Insert(1, opponentGrid);
            Canvas.SetLeft(opponentGrid, GreenLeft);
            Canvas.SetTop(opponentGrid, GreenTop);
        }

        private async void GameSetup()
        {
            gameTimer.Tick += GameLoop;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20); ///will tick every 20ms
          
            gameTimer.Start();
        }

        private async void GameLoop(object? sender, EventArgs e)
        {
            if (Coins.Count == 0) 
            {
                return;
            }
            if(_name.Length > 0)
            {
                if (_name.ToString() == "Apple.")
                {
                    ApplesExpression.Interpret(Apples, _connection);
                    _name = string.Empty;
                }
                else if (_name.ToString() == "RottenApple.")
                {
                    RottenApplesExpression.Interpret(RottenApples, _connection);
                    _name = string.Empty;
                }
                else if (_name.ToString() == "Cherry.")
                {
                    CherriesExpression.Interpret(Cherries, _connection);
                    _name = string.Empty;
                }
                else if (_name.ToString() == "Strawberry.")
                {
                    StrawberriesExpression.Interpret(Strawberries, _connection);
                    _name = string.Empty;
                }
            }
            Canvas.SetLeft(mainGrid, YellowLeft);
            Canvas.SetTop(mainGrid, YellowTop);
            Canvas.SetLeft(opponentGrid, GreenLeft);
            Canvas.SetTop(opponentGrid, GreenTop);

            int AppHeight = 600;
            int AppWidth = 800;
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

            if (goDown && YellowTop + 30 > AppHeight)
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
                    handler.Handle(ref pacman, item);
                    IDecorator grid = new ShowSpeed(new AddLabel(new AddHealthBar(pacman, pacman.Health)), pacman.Speed.ToString());
                    mainGrid = grid.Draw();
                    LayoutRoot.Children.Remove(LayoutRoot.Children[0]);
                    LayoutRoot.Children.Insert(0, mainGrid);
                    Canvas.SetLeft(mainGrid, YellowLeft);
                    Canvas.SetTop(mainGrid, YellowTop);
                    await _connection.InvokeAsync("SendCommand", pacman.Speed.ToString(), "ChangeSpeedLabel");
                    var index = Apples.IndexOf(Apples
                        .Where(a => a.Top == item.Top 
                                && a.Left == item.Left)
                        .FirstOrDefault());
                    Apples.RemoveAt(index);

                    string serializedRemoveAppleAtIndexCommand = JsonSerializer.Serialize(new RemoveAppleAtIndexCommand(index));
                    await _connection.InvokeAsync("SendCommand", serializedRemoveAppleAtIndexCommand, "RemoveAppleAtIndex");
                    break;
                }
            }

            foreach (var item in RottenApples)
            {
                Rect hitBox = new Rect(item.Left, item.Top, 10, 10);
                if (pacmanHitBox.IntersectsWith(hitBox))
                {
                    handler.Handle(ref pacman, item);
                    IDecorator grid = new ShowSpeed(new AddLabel(new AddHealthBar(pacman, pacman.Health)), pacman.Speed.ToString());
                    mainGrid = grid.Draw();
                    LayoutRoot.Children.Remove(LayoutRoot.Children[0]);
                    LayoutRoot.Children.Insert(0, mainGrid);
                    Canvas.SetLeft(mainGrid, YellowLeft);
                    Canvas.SetTop(mainGrid, YellowTop);
                    await _connection.InvokeAsync("SendCommand", pacman.Speed.ToString(), "ChangeSpeedLabel");
                    var index = RottenApples.IndexOf(RottenApples.Where(a => a.Top == item.Top && a.Left == item.Left).FirstOrDefault());
                    RottenApples.RemoveAt(index);

                    string serializedRemoveRottenAppleAtIndexCommand = JsonSerializer.Serialize(new RemoveRottenAppleAtIndexCommand(index));
                    await _connection.InvokeAsync("SendCommand", serializedRemoveRottenAppleAtIndexCommand, "RemoveRottenAppleAtIndex");
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

                    string serializedRemoveCoinAtIndexCommand = JsonSerializer.Serialize(new RemoveCoinAtIndexCommand(index));
                    await _connection.InvokeAsync("SendCommand", serializedRemoveCoinAtIndexCommand, "RemoveCoinAtIndex");
                    pacman.Score += item.Value;
                    score = pacman.Score;
                    await _connection.InvokeAsync("SendCommand", JsonSerializer.Serialize(new GivePointsToOpponentCommand(score)), "GivePointsToOpponent");

                    break;
                }
            }

            foreach (var item in Cherries)
            {
                Rect hitBox = new Rect(item.Left, item.Top, 10, 10);
                if (pacmanHitBox.IntersectsWith(hitBox))
                {
                    handler.Handle(ref pacman, item);
                    score = pacman.Score;
                    await _connection.InvokeAsync("SendCommand", JsonSerializer.Serialize(new GivePointsToOpponentCommand(score)), "GivePointsToOpponent");
                    var index = Cherries.IndexOf(Cherries.Where(a => a.Top == item.Top && a.Left == item.Left).FirstOrDefault());
                    Cherries.RemoveAt(index);
                    string serializedRemoveCherryAtIndexCommand = JsonSerializer.Serialize(new RemoveCherryAtIndexCommand(index));
                    await _connection.InvokeAsync("SendCommand", serializedRemoveCherryAtIndexCommand, "RemoveCherryAtIndex");
                    break;
                }
            }
            foreach (var item in Strawberries)
            {
                Rect hitBox = new Rect(item.Left, item.Top, 10, 10);
                if (pacmanHitBox.IntersectsWith(hitBox))
                {
                    handler.Handle(ref pacman, item);
                    var index = Strawberries.IndexOf(Strawberries.Where(a => a.Top == item.Top && a.Left == item.Left).FirstOrDefault());
                    Strawberries.RemoveAt(index);
                    await _connection.InvokeAsync("SendRemoveStrawberryAtIndex", new RemoveStrawberryAtIndexCommand(index));
                    break;
                }
            }

            foreach (var item in Walls)
            {
                Rect hitBox = new Rect(item.Left, item.Top, 10, 10);
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
                    int index = Walls.IndexOf(Walls.Where(a => a.Top == item.Top && a.Left == item.Left).FirstOrDefault());
                    await _connection.InvokeAsync("SendMakeVisitWallCommand", index.ToString());
                    item.Accept(new WallVisitor());
                    break;
                }
            }

            int mobIndex = 0;
            foreach (var item in Mobs)
            {
                Rect hitBox = new Rect(item.Left, item.Top, 10, 10);
                if (pacmanHitBox.IntersectsWith(hitBox))
                {
                    pacman.Health -= item.GetDamage();
                    await _connection.InvokeAsync("PacmanDamage", pacman.Health);
                    IDecorator grid = new ShowSpeed(new AddLabel(new AddHealthBar(pacman, pacman.Health)), pacman.Speed.ToString());
                    mainGrid = grid.Draw();
                    LayoutRoot.Children.Remove(LayoutRoot.Children[0]);
                    LayoutRoot.Children.Insert(0, mainGrid);
                    Canvas.SetLeft(mainGrid, YellowLeft);
                    Canvas.SetTop(mainGrid, YellowTop);
                    await _connection.InvokeAsync("SendCommand", pacman.Speed.ToString(), "ChangeSpeedLabel");
                    if (pacman.Health < 0)
                    {
                        YellowLeft = 0;
                        YellowTop = 0;
                        pacman.Health = 100;
                        grid = new ShowSpeed(new AddLabel(new AddHealthBar(pacman, pacman.Health)), pacman.Speed.ToString());
                        mainGrid = grid.Draw();
                        LayoutRoot.Children.Remove(LayoutRoot.Children[0]);
                        LayoutRoot.Children.Insert(0, mainGrid);
                        Canvas.SetLeft(mainGrid, YellowLeft);
                        Canvas.SetTop(mainGrid, YellowTop);
                        await _connection.InvokeAsync("PacmanDamage", pacman.Health);
                        await _connection.InvokeAsync("SendCommand", pacman.Speed.ToString(), "ChangeSpeedLabel");
                    }
                    int index = Mobs.IndexOf(Mobs.Where(a => a.Top == item.Top && a.Left == item.Left).FirstOrDefault());
                    await _connection.InvokeAsync("SendMakeVisitMobCommand", index.ToString());
                    item.Accept(new MobVisitor());
                }
                if (_connection.State.HasFlag(HubConnectionState.Connected))
                {
                    if (item.GoLeft && item.Left + 40 > AppWidth)
                    {
                        string a = JsonConvert.SerializeObject(new { Position = item.Left, Index = mobIndex, GoLeft = false });
                        await _connection.InvokeAsync("SendCommand", a, "Move");
                    }
                    else if (!item.GoLeft && item.Left - 5 < 1)
                    {
                        string a = JsonConvert.SerializeObject(new { Position = item.Left, Index = mobIndex, GoLeft = true });
                        await _connection.InvokeAsync("SendCommand", a, "Move");
                    }
                    if (item.GoLeft)
                    {
                        string a = JsonConvert.SerializeObject(new { Position = item.Left + item.GetSpeed(), Index = mobIndex, GoLeft = true });
                        await _connection.InvokeAsync("SendCommand", a, "Move");
                    }
                    else
                    {
                        string a = JsonConvert.SerializeObject(new { Position = item.Left - item.GetSpeed(), Index = mobIndex, GoLeft = false });
                        await _connection.InvokeAsync("SendCommand", a, "Move");
                    }
                    mobIndex++;
                }
            }

            int spikeIndex = 0;
            foreach (var item in Spikes)
            {
                Rect hitBox = new Rect(item.Left, item.Top, 10, 10);
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
                    int index = Spikes.IndexOf(Spikes.Where(a => a.Top == item.Top && a.Left == item.Left).FirstOrDefault());
                    await _connection.InvokeAsync("SendMakeVisitSpikeCommand", index.ToString());
                    item.Accept(new SpikeVisitor());
                    break;
                }
                if (item.GoLeft && item.Left + 40 > AppWidth)
                {
                    string a = JsonConvert.SerializeObject(new { Position = item.Left, Index = spikeIndex, GoLeft = false });
                    await _connection.InvokeAsync("SendCommand", a, "MoveObstacle");
                }
                else if (!item.GoLeft && item.Left - 5 < 1)
                {
                    string a = JsonConvert.SerializeObject(new { Position = item.Left, Index = spikeIndex, GoLeft = true });
                    await _connection.InvokeAsync("SendCommand", a, "MoveObstacle");
                }
                if (item.GoLeft)
                {
                    string a = JsonConvert.SerializeObject(new { Position = item.Left + 3, Index = spikeIndex, GoLeft = true });
                    await _connection.InvokeAsync("SendCommand", a, "MoveObstacle");
                }
                else
                {
                    string a = JsonConvert.SerializeObject(new { Position = item.Left - 3, Index = spikeIndex, GoLeft = false });
                    await _connection.InvokeAsync("SendCommand", a, "MoveObstacle");
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

        public override void AddApple(AddAppleCommand cmd)
        {
            cmd.SetApplesListCopy(copyApples);
            cmd.Execute(Apples);
        }

        public override void AddRottenApple(AddRottenAppleCommand command)
        {
            command.SetRottenApplesListCopy(copyRottenApples);
            command.Execute(RottenApples);
        }

        public override void AddCherry(AddCherryCommand command)
        {
            command.SetCherriesListCopy(copyCherries);
            command.Execute(Cherries);
        }

        public override void AddStrawberry(AddStrawberyCommand command)
        {
            command.SetStrawberriesListCopy(copyStrawberries);
            command.Execute(Strawberries);
        }

        public override void VisitWall(string command)
        {
            MakeVisitWallCommand commanda = new MakeVisitWallCommand(command);
            commanda.Execute(Walls);
        }

        public override void VisitSpike(string command)
        {
            MakeVisitSpikeCommand commanda = new MakeVisitSpikeCommand(command);
            commanda.Execute(Spikes);
        }

        public override void VisitMob(string command)
        {
            MakeVisitMobCommand commanda = new MakeVisitMobCommand(command);
            commanda.Execute(Mobs);
        }
    }
}



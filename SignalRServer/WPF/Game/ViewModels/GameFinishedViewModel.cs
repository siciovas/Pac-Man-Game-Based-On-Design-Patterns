using ClassLibrary._Pacman;
using ClassLibrary.Commands;
using ClassLibrary.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Game.ViewModels
{
    public class GameFinishedViewModel : LevelViewModelBase
    {
        public GameFinishedViewModel(int score, int opScore)
        {
            Score = score;
            OpScore = opScore;
        }

        public int Score { get; }
        public override int score { get => 1; set { } }
        public int OpScore { get; }
        public override int opponentScore { get => 1; set { } }

        public override void AddApple(AddAppleCommand command)
        {
            throw new NotImplementedException();
        }

        public override void AddCherry(AddCherryCommand command)
        {
            throw new NotImplementedException();
        }

        public override void AddRottenApple(AddRottenAppleCommand command)
        {
            throw new NotImplementedException();
        }

        public override void AddStrawberry(AddStrawberyCommand command)
        {
            throw new NotImplementedException();
        }

        public override void ChangeSpeed(string speed)
        {
            throw new NotImplementedException();
        }

        public override void DamagePacman(int damage)
        {
            return;
        }

        public override void Move(string pos)
        {
            return;
        }

        public override void MoveObstacle(string serializedObject)
        {
            throw new NotImplementedException();
        }

        public override void OnDownClick()
        {
            return;
        }

        public override void OnLeftClick()
        {
            return;
        }

        public override void OnRightClick()
        {
            return;
        }

        public override void OnUpClick()
        {
            return;
        }

        public override void RemoveApple(string command)
        {
            return;
        }

        public override void RemoveCherry(string command)
        {
            return;
        }

        public override Task RemoveCoin(string command)
        {
            return Task.CompletedTask;
        }

        public override void RemoveStrawberry(RemoveStrawberryAtIndexCommand command)
        {
            throw new NotImplementedException();
        }

        public override void RottenApple(string command)
        {
            return;
        }

        public override void SendOponmentCoordinates(string serializedObject)
        {
            return;
        }

        public override void UpdateOpScore(string command)
        {
            return;
        }

        public override void VisitMob(string command)
        {
            throw new NotImplementedException();
        }

        public override void VisitSpike(string command)
        {
            throw new NotImplementedException();
        }

        public override void VisitWall(string command)
        {
            throw new NotImplementedException();
        }
    }
}

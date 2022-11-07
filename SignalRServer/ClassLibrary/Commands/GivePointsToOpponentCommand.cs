using ClassLibrary._Pacman;
using System;

namespace ClassLibrary.Commands
{
    public class GivePointsToOpponentCommand : ICommand
    {
        public int Score { get; set; }

        public GivePointsToOpponentCommand(int score)
        {
            Score = score;
        }

        public void Execute(object parameter)
        {
            var pacman = (Pacman)parameter;
            pacman.Score = Score;
        }
    }
}

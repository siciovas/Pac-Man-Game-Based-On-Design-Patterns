using ClassLibrary.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Game.Stores;
using WPF.Game.ViewModels;

namespace WPF.Game.Navigation
{
    public class GameFinishedState : State
    {
        private int _score;
        private int _opScore;
        LevelsFacade _context;
        public GameFinishedState(int score, int opScore, LevelsFacade levelsFacade)
        {
            _score = score;
            _opScore = opScore; 
            _context = levelsFacade;
        }

        public override void GoToView()
        {
            _context.CurrentViewModel = new GameFinishedViewModel(_score, _opScore);
        }
    }
}

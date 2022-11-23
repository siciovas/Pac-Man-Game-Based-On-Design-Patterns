using ClassLibrary.Views;
using WPF.Connection;
using WPF.Game.Stores;
using WPF.Game.ViewModels;

namespace WPF.Game.Navigation
{
    public class PlayingState : State
    {
        private int _level;
        public FirstLevelViewModel _firstLevelViewModel;
        public LevelViewModelBase _secondLevelViewModel;
        public LevelViewModelBase _thirdLevelViewModel;
        public LevelViewModelBase _fourthLevelViewModel;
        public LevelViewModelBase _fifthLevelViewModel;

        private IConnectionProvider _connectionProvider;

        LevelsFacade _context;

        public PlayingState(IConnectionProvider connectionProvider, LevelsFacade context)
        {
            _level = 0;
            _connectionProvider = connectionProvider;
            _context = context;
        }
        //return level 1-5
        public override void GoToView()
        {
            _level++;
            if(_level == 1)
            {
                _firstLevelViewModel = new FirstLevelViewModel(_connectionProvider);
                _context.CurrentViewModel = _firstLevelViewModel;
                return;
            }
            if (_level == 2)
            {
                _secondLevelViewModel = new SecondLevelViewModel(_connectionProvider, _firstLevelViewModel.score, _firstLevelViewModel.opponentScore);
                _context.CurrentViewModel = _secondLevelViewModel;
                return;
            }
            if (_level == 3)
            {
                _thirdLevelViewModel = new ThirdLevelViewModel(_connectionProvider, _secondLevelViewModel.score, _secondLevelViewModel.opponentScore);
                _context.CurrentViewModel = _thirdLevelViewModel;
                return;
            }
            if (_level == 4)
            {
                _fourthLevelViewModel = new FourthLevelViewModel(_connectionProvider, _thirdLevelViewModel.score, _thirdLevelViewModel.opponentScore);
                _context.CurrentViewModel = _fourthLevelViewModel;
                return;
            }
            if (_level == 5)
            { 
                _fifthLevelViewModel = new FifthLevelViewModel(_connectionProvider, _fourthLevelViewModel.score, _fourthLevelViewModel.opponentScore);
                _context.CurrentViewModel = _fifthLevelViewModel;
                return;
            }

            _context.TransitionTo(new GameFinishedState(_fifthLevelViewModel.score, _fifthLevelViewModel.opponentScore, _context));          
        }
    }
}

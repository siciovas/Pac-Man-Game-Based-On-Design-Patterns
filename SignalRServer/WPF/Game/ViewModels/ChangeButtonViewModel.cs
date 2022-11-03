using ClassLibrary.Views;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF.Game.Stores;

namespace WPF.Game.ViewModels
{
    public class ChangeButtonViewModel : ViewModelBase
    {
        public ICommand NextView { get; set; }
        private readonly LevelsFacade _levelsFacade;

        public ChangeButtonViewModel(LevelsFacade levelsFacade)
        {
            NextView = new RelayCommand(new Action(GoToNextView));
            _levelsFacade = levelsFacade;
        }

        private void GoToNextView()
        {
            _levelsFacade.ChangeLevel();
        }
    }
}

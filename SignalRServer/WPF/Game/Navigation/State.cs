using ClassLibrary.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Game.Stores;

namespace WPF.Game.Navigation
{
    public abstract class State
    {
        protected LevelsFacade _context;

        public void SetContext(LevelsFacade context)
        {
            this._context = context;
        }
        public abstract void GoToView();
    }
}

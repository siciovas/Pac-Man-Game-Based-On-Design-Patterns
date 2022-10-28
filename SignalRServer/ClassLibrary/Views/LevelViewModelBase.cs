using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Views
{
    public abstract class LevelViewModelBase : ViewModelBase
    {
        public abstract void OnRightClick();
        public abstract void OnDownClick();
        public abstract void OnUpClick();
        public abstract void OnLeftClick();

    }
}

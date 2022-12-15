using ClassLibrary.Bridge;
using ClassLibrary.MainUnit;
using ClassLibrary.Visitor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Commands
{
    public class MakeVisitWallCommand : ICommand
    {
        public string _index { get; set; }
        public MakeVisitWallCommand(string index)
        {
            _index = index;
        }

        public void Execute(object parameter)
        {
            var unit = (ObservableCollection<Wall>)parameter;
            unit[Int32.Parse(_index)].Accept(new WallVisitor());
        }

        public void Undo(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}

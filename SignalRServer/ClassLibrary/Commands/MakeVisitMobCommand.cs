using ClassLibrary.Bridge;
using ClassLibrary.Mobs;
using ClassLibrary.Visitor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Commands
{
    public class MakeVisitMobCommand : ICommand
    {
        public string _index { get; set; }
        public MakeVisitMobCommand(string index)
        {
            _index = index;
        }

        public void Execute(object parameter)
        {
            var unit = (ObservableCollection<Mob>)parameter;
            unit[Int32.Parse(_index)].Accept(new MobVisitor());
        }

        public void Undo(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}

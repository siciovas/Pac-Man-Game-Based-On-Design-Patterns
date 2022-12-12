using ClassLibrary.Commands;
using ClassLibrary.Fruits;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Interpreter
{
    public class CherriesRestoreExpression : Expression
    {
        private readonly List<Cherry> CherriesList;

        public CherriesRestoreExpression(List<Cherry> applesList)
        {
            CherriesList = applesList;
        }

        public override async void Interpret(object _object, HubConnection connection)
        {
            var apples = (ObservableCollection<Cherry>)_object;
            int index = 0;
            foreach (var item in CherriesList)
            {
                if (!apples.Any(x => x.Left == item.Left && x.Top == item.Top))
                {
                    apples.Add(item);
                    await connection.InvokeAsync("SendAddCherry", new AddCherryCommand(index));
                    return;
                }
                index++;
            }
        }
    }
}

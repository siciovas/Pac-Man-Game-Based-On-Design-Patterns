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
    public class StrawberriesRestoreExpression : Expression
    {
        private readonly List<Strawberry> StrawberriesList;

        public StrawberriesRestoreExpression(List<Strawberry> applesList)
        {
            StrawberriesList = applesList;
        }

        public override async void Interpret(object _object, HubConnection connection)
        {
            var apples = (ObservableCollection<Strawberry>)_object;
            int index = 0;
            foreach (var item in StrawberriesList)
            {
                if (!apples.Any(x => x.Left == item.Left && x.Top == item.Top))
                {
                    apples.Add(item);
                    await connection.InvokeAsync("SendAddStrawberry", new AddRottenAppleCommand(index));
                    return;
                }
                index++;
            }
        }
    }
}

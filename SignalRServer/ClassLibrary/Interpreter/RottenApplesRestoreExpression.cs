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
    public class RottenApplesRestoreExpression : Expression
    {
        private readonly List<RottenApple> RottenApplesList;

        public RottenApplesRestoreExpression(List<RottenApple> applesList)
        {
            RottenApplesList = applesList;
        }

        public override async void Interpret(object _object, HubConnection connection)
        {
            var apples = (ObservableCollection<RottenApple>)_object;
            int index = 0;
            foreach (var item in RottenApplesList)
            {
                if (!apples.Any(x => x.Left == item.Left && x.Top == item.Top))
                {
                    apples.Add(item);
                    await connection.InvokeAsync("SendAddRottenApple", new AddRottenAppleCommand(index));
                    return;
                }
                index++;
            }
        }
    }
}

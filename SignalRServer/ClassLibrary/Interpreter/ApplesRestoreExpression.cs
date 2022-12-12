using ClassLibrary._Pacman;
using ClassLibrary.Commands;
using ClassLibrary.Fruits;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClassLibrary.Interpreter
{
    public class ApplesRestoreExpression : Expression
    {
        private readonly List<Apple> ApplesList;

        public ApplesRestoreExpression(List<Apple> applesList)
        {
            ApplesList = applesList;
        }

        public override async void Interpret(object _object, HubConnection connection)
        {
            var apples = (ObservableCollection<Apple>)_object;
            int index = 0;
            foreach (var item in ApplesList)
            {
                if(!apples.Any(x => x.Left == item.Left && x.Top == item.Top))
                {
                    apples.Add(item);
                    await connection.InvokeAsync("SendAddApple", new AddAppleCommand(index));
                    return;
                }
                index++;
            }
        }
    }
}

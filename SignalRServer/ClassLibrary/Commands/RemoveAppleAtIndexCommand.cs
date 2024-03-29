﻿using ClassLibrary.Fruits;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace ClassLibrary.Commands
{
    public class RemoveAppleAtIndexCommand : ICommand
    {
        public int Index { get; set; }
        public RemoveAppleAtIndexCommand(int index)
        {
            Index = index;
        }
        public void Execute(object parameter)
        {
            var apples = (ObservableCollection<Apple>)parameter;
            apples.RemoveAt(Index);
        }

        public void Undo(object parameter)
        {
            
        }
    }
}

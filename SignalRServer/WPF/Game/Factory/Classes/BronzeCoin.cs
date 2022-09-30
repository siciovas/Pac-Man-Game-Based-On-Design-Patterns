using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF.Game.Factory.Interfaces;

namespace WPF.Game.Factory.Classes
{
    public class BronzeCoin : ICoin
    {
        public BronzeCoin()
        {
            Value = 2;
        }
        public int Value { get; set; }
    }
}

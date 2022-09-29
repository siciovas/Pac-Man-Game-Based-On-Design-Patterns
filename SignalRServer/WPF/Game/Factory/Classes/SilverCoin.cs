using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using WPF.Game.Factory.Interfaces;

namespace WPF.Game.Factory.Classes
{
    public class SilverCoin : ICoin
    {
        public SilverCoin()
        {
            Value = 1;
        }
        public int Value { get; set; }
    }
}

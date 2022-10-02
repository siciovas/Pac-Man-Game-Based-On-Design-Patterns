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
        public SilverCoin(int left, int top)
        {
            Value = 2;
            Color = "silver";
            Left = left;
            Top = top;
        }
        public int Value { get; set; }
        public double Left { get; set; }
        public double Top { get; set; }
        public string Color { get; set; }
    }
}

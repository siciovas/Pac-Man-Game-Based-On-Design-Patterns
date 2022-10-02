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
        public BronzeCoin(int left, int top)
        {
            Value = 2;
            Color = "RosyBrown";
            Left = left;
            Top = top;  
        }
        public int Value { get; set; }
        public double Left { get; set; }
        public double Top { get; set; }    
        public string Color { get; set; }
    }
}

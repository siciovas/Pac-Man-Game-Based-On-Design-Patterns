using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF.Game.Factory.Interfaces
{
    public interface ICoin
    {
        int Value { get; set; }
        double Top { get; set; }
        double Left { get; set; }
        string Color { get; set; }
    }
}

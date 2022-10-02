using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WPF.Game.AbstractFactory.Interfaces
{
    public interface IGhost
    {
        public int Speed { get; set; }
        public int Top { get; set; }
        public int Left { get; set; }
        public ImageBrush Fill { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ClassLibrary.MainUnit
{
    public abstract class Unit
    {
        public int Top { get; set; }
        public int Left { get; set; }
        public ImageBrush Appearance { get; set; }
        public string Name { get; set; }
    }
}

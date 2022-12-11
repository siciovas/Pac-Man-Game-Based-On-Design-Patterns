using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary._Pacman
{
    public class Coordinates
    {
        public int Top { get; set; }
        public int Left { get; set; }

        public Coordinates(int top, int left)
        {
            Top = top;
            Left = left;
        }   
    }
}

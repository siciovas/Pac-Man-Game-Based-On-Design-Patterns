using System;
using System.Collections.Generic;
using System.Windows.Shapes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ClassLibrary.Decorator
{
    public abstract class IDecorator
    {
        public abstract Grid Draw();
    }
}

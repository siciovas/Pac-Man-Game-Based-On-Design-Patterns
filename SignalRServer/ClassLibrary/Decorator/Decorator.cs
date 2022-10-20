using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace ClassLibrary.Decorator
{
    public abstract class Decorator : IDecorator
    {
        protected IDecorator decoratedShape;

        public Decorator(IDecorator decoratedShape)
        {
            this.decoratedShape = decoratedShape;
        }

        public override Grid Draw()
        {
            return decoratedShape.Draw();
        }
    }
}

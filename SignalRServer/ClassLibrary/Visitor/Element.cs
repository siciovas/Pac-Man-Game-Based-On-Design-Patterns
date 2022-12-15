using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Visitor
{
    public interface Element
    {
        public void Accept(IVisitor visitor);
    }
}

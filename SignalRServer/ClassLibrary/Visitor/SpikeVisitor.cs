﻿using ClassLibrary.MainUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Visitor
{
    public class SpikeVisitor : IVisitor
    {
        public void Visit(Unit unit)
        {
            unit.Top = 530;
        }
    }
}

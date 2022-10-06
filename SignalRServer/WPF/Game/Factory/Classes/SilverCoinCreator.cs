using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Game.Factory.Interfaces;

namespace WPF.Game.Factory.Classes
{
    class SilverCoinCreator : CoinFactory
    {
        public override ICoin GetCoin(int left, int top)
        {
            return new SilverCoin(left, top);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Game.Factory.Interfaces;

namespace WPF.Game.Factory.Classes
{
    class ThirdLevelCoinCreator : CoinFactory
    {
        public override ICoin GetCoin(int left, int top)
        {
            return new GoldCoin(left, top);
        }
    }
}

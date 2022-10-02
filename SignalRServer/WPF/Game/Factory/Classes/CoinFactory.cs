using System.Collections.ObjectModel;
using WPF.Game.Factory.Interfaces;

namespace WPF.Game.Factory.Classes
{
    public abstract class CoinFactory
    {
        abstract public ICoin GetCoin(int left, int top);
    }
}

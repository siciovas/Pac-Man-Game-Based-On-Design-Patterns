using ClassLibrary.MainUnit;
using System.Windows.Media;

namespace ClassLibrary.Coins.Interfaces
{
    public abstract class Coin : Unit
    {

        public int Value { get; set; }
       /* public abstract Coin Copy();*/
        //protected Coin(int top, int left, ImageBrush appearance) : base(top, left, appearance) { }

        protected Coin() { }
    }
}

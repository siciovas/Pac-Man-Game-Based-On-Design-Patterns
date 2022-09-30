using System;
using System.Windows;
using WPF.Game.Factory.Interfaces;

namespace WPF.Game.Factory.Classes
{
    public class GoldCoin : ICoin
    {
        public GoldCoin()
        {
            Value = 5;
        } 
        public int Value { get; set; }
    }
}

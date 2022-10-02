using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF.Game.Factory.Interfaces;

namespace WPF.Game.ViewModels
{
    public class CoinViewModel
    {
        private string _color;
        private int _value;
        private int _top;
        private int _left;
        private Visibility _visibility;

        public string Color 
        { 
            get => _color; 
            set => _color = value; 
        }
        public int Top
        {
            get => _top;
            set => _top = value;
        }
        public int Left
        {
            get => _left;
            set => _left = value;
        }
        public int Value 
        { 
            get => _value; 
            set => _value = value; 
        }
        public Visibility Visible 
        { 
            get => _visibility; 
            set => _visibility = value; 
        }

        public CoinViewModel(ICoin coin, int top, int left)
        {
            Color = coin.Color;
            Value = coin.Value;
            Left = left;
            Top = top;
            Visible = Visibility.Visible;
        }

        public void ChangeVisibility()
        {
            _visibility = Visibility.Collapsed;
        }
    }
}

using ClassLibrary.MainUnit;
using ClassLibrary.Strategies;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System;
using ClassLibrary.Fruits;
using Microsoft.AspNetCore.SignalR.Protocol;
using ClassLibrary.Memento;

namespace ClassLibrary._Pacman
{
    public class Pacman : Unit
    {
        public int Speed { get; set; }
        public int Score { get; set; }
        public int Health { get; set; }
        public bool GhostMode { get; set; }

        private (int, int) _state;

        private Algorithm algorithm;

        public Algorithm GetAlgorithm()
        {
            return algorithm;
        }

        public void SetAlgorithm(Algorithm algorithm)
        {
            this.algorithm = algorithm;
        }

        public Pacman(string name) 
        {
            UnitType = new UnitType();
            Speed = 8;
            Score = 0;
            Health = 100;
            GhostMode = false;
            UnitType.Name = name;
            Health = 100;
            ImageBrush pacmanBrush = new ImageBrush();
            pacmanBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/pacman.jpg"));
            UnitType.Appearance = pacmanBrush;
            _state = (Top, Left);
        }

        public void Action(ref Pacman pacman)
        {
            algorithm.BehaveDifferently(ref pacman);
        }

        public override Pacman Copy()
        {
            //creates a green copy of pacman using deep copy
            var clone = (Pacman)this.MemberwiseClone();
            UnitType unt = new UnitType();
            clone.UnitType = unt;
            clone.algorithm = new DefaultAlgorithm();
            clone.UnitType.Appearance = new ImageBrush();
            ImageBrush pacmanBrush = new ImageBrush();
            pacmanBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/pacmanOp.jpg"));
            clone.UnitType.Appearance = pacmanBrush;
            clone.UnitType.Name = "Opponent";
            return clone;
        }

        // Saves the current state inside a memento.
        public IMemento Save()
        {
            return new ConcreteMemento(this.Top, this.Left);
        }

        // Restores the Originator's state from a memento object.
        public void Restore(IMemento memento)
        {
            if (!(memento is ConcreteMemento))
            {
                throw new Exception("Unknown memento class " + memento.ToString());
            }

            var state = memento.GetState();
            this.Top = state.Item1;
            this.Left = state.Item2;
        }
    }
}

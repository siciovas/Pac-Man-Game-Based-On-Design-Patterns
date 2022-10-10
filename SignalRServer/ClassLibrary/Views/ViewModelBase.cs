using System;
using System.ComponentModel;

namespace ClassLibrary.Views
{
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public virtual void Dispose() { }

        public abstract void OnRightClick();
        public abstract void OnDownClick();

        public abstract void OnUpClick();

        public abstract void OnLeftClick();

    }
}

using System;
using System.Threading.Tasks;

namespace ClassLibrary.Observer
{
    public interface ISubject
    {
        public void Notify();
        public Task OnConnectedAsync();
        public Task OnDisconnectedAsync(Exception? exception);
        public void Subscribe();
        public void Unsubscribe();
    }
}

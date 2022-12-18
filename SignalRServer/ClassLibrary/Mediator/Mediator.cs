using System.Threading.Tasks;

namespace ClassLibrary.Mediator
{
    public interface IMediator
    {
        Task SendCommand(string command, string name);
    }
}

using System.Threading.Tasks;

namespace UserApplication.WebBackend
{
    public interface IEmitStatusUpdates
    {
        Task PushStatusUpdate(string status);
    }
}
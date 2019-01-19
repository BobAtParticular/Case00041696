using Microsoft.AspNet.SignalR;

namespace UserApplication.WebBackend
{
    public class UserApplicationHub :
        Hub<IEmitStatusUpdates>
    {
    }
}
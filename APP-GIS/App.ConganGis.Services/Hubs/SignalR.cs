using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace App.CongAnGis.Services.Hubs
{
    [HubName("hub")]
    public class SignalR : Hub
    {
        public async Task Message(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}

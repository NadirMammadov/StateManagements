using Microsoft.AspNetCore.SignalR;
using StateManagements.Session_.SessionExtension;
namespace StateManagements.Session_.Hubs
{
    public class ChatHub : Hub
    {
        public async Task RunCartBox()
        {
            //var itemcount = Context.GetHttpContext().Session.Get<List<Cart>>("_cart").Count;
            var itemcount = 5;
            await Clients.All.SendAsync("ShowCartItemCount", itemcount);
        }
    }
}

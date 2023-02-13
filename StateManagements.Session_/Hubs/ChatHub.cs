using Microsoft.AspNetCore.SignalR;
using StateManagements.Session_.SessionExtension;
namespace StateManagements.Session_.Hubs
{
    public class ChatHub : Hub
    {
        public async Task RunNese(int ss)
        {
            var items = Context.GetHttpContext().Session.Get<List<Cart>>("_cart");
            if(items==null)
            {
                items = new List<Cart>();
            }
            var itemcount = 1;

            decimal total = 1;
            if (items!=null)
            {
                itemcount = items.Count;
                foreach (var item in items)
                {
                    total += item.Count * item.Price;
                }

            }
       
            
            await Clients.All.SendAsync("ShowCartItemCount", itemcount,total,items);
        }
    }
}

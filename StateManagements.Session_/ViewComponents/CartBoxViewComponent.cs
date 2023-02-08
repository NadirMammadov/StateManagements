using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StateManagements.Session_.SessionExtension;

namespace StateManagements.Session_.ViewComponents
{
    public class CartBoxViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data =  HttpContext.Session.Get<List<Cart>>("_cart");
            return View(data);
        }
    }
}

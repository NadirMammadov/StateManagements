using Microsoft.AspNetCore.Mvc;
namespace StateManagements.Session_.ViewComponents;
public class BestSellerViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}

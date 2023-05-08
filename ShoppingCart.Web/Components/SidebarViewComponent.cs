using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.DataAccess.Model;
namespace ShoppingCart.Web.Components
{
    public class SidebarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var sidebar = new List<SidebarItem>
        {
            new SidebarItem
            {
                Id = 1,
                Name = "Category",
                IsActive = true,
                DisplayPriority = 1,
                Controller = "Category",
                Action = "Index",
                Class = "fa-solid fa-list" 
            }
        };
            sidebar.Add(new SidebarItem { Id = 2, Name = "Product", IsActive = true, DisplayPriority = 2, Controller = "Product", Action = "Index", Class = "fa-brands fa-product-hunt" });
            return View(sidebar);
        }
    }
}






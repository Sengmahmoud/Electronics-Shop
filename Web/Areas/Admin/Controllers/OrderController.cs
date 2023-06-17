using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IOredrService _orderService;

        public OrderController(IOredrService orderService)
        {
            _orderService = orderService;
        }
        public IActionResult Index(int page=1,int pageSize=5)
        {
            var model = new OrderViewModel()
            {
                Orders = _orderService.GetOrders(page,pageSize)
            };

            return View(model);
        }
    }
}

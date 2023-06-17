using Core.Dtos.Order;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly IOredrService _oredrService;
        private readonly ICategoryService _categoryService;

        public HomeController(IProductService productService, IOredrService oredrService,ICategoryService categoryService)
        {
            _productService = productService;
            _oredrService = oredrService;
            _categoryService = categoryService;
        }
        public IActionResult Index(int? categoryId, int page = 1, int pageSize = 5)
        {
            var model = new HomeViewModel()
            {
                Products = _productService.GetProducts(categoryId, page, pageSize)
            };
            FillUp(model);
            return View(model);
        }
        public IActionResult BuyNow(Guid productId)
        {
            var model = new HomeViewModel()
            {
                OrderInput=new OrderInputDto()
                {
                  ProductId=productId
                }
            };
            return View(model);
        }
        [HttpPost]
        
        public IActionResult BuyNow(HomeViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   
                    _oredrService.BuyProduct(model.OrderInput);
                    model.Message = "Added Successfully";
                    model.Success = true;
                }

                catch (Exception ex)
                {
                    model.Message = "GenericError";
                    model.Success = false;
                }
            }
            else
            {
                model.Message = "InvalidInputs";
                model.Success = false;
            }
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Buy(Guid productId,int qnty)
        {
            try
            {
                var orderInput = new OrderInputDto()
                {
                    ProductId = productId,
                    Qantity= qnty
                };
                _oredrService.BuyForCustomer(orderInput);
                return Json(new
                {
                    Message = "Done , We Will Contact You",
                    Success = true
                });
            }

            catch (Exception ex)
            {

                return Json(new
                {
                    Message = "GenericError",
                    Success = false
                });
            }
        }
        private void FillUp(HomeViewModel model)
        {
            model.CategoriesList = _categoryService.GetCategories()
                    .Select(c => new SelectListItem()
                    {
                        Text = c.Name,
                        Value = c.Id.ToString()
                    }).ToList();
        }
    }
}

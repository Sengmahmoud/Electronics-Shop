using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(ILogger<ProductController> ilogger, IProductService productService, ICategoryService categoryService)
        {
            _logger = ilogger;
            _productService = productService;
            _categoryService = categoryService;
        }
        [Authorize(Permissions.Products.Defualt)]
        public IActionResult Index(int? categoryId, int page = 1, int PageSize = 5)
        {
            var model = new ProductViewModel()
            {
                Products = _productService.GetProducts(categoryId, page, PageSize),
                CategoryId = categoryId,

            };
            FillUp(model);
            return View(model);
        }
        [Authorize(Permissions.Products.Add)]
        public IActionResult Create()
        {
            var model = new ProductViewModel()
            {
                Product = new ProductInputDto()
            };
            FillUp(model);
            return View(model);
        }
        [HttpPost]
        [Authorize(Permissions.Products.Add)]
        public IActionResult Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.Product.Image != null)
                    {
                        var fileName = $"{Guid.NewGuid()}-{model.Product.Image.FileName}";
                        var filepath =
                            new PhysicalFileProvider(
                                    System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Products"))
                                .Root + $@"\{fileName}";

                        using FileStream fs = System.IO.File.Create(filepath);
                        model.Product.Image.CopyTo(fs);
                        fs.Flush();
                        model.Product.ImagePath = fileName;
                    }
                    _productService.AddProduct(model.Product);
                    model.Message = "Added Successfully";
                    model.Success = true;
                }

                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, ex);
                    model.Message = "GenericError";
                    model.Success = false;
                }
            }
            else
            {
                model.Message = "InvalidInputs";
                model.Success = false;
            }


            FillUp(model);
            return View(model);
        }
        [Authorize(Permissions.Products.Edit)]
        public IActionResult Edit(Guid id)
        {
            var model = new ProductViewModel()
            {
                Product = _productService.GetProduct(id)
            };
            FillUp(model);
            return View(model);
        }

        [HttpPost]
        [Authorize(Permissions.Products.Edit)]
        public IActionResult Edit(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var product = _productService.GetProduct(model.Product.Id);
                    if (model.Product.Image != null)
                    {
                        var fileName = $"{Guid.NewGuid()}-{model.Product.Image.FileName}";
                        var filepath =
                            new PhysicalFileProvider(
                                    System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Products"))
                                .Root + $@"\{fileName}";

                        using FileStream fs = System.IO.File.Create(filepath);
                        model.Product.Image.CopyTo(fs);
                        fs.Flush();
                        model.Product.ImagePath = fileName;
                        if (product != null && !string.IsNullOrWhiteSpace(product.ImagePath))
                        {
                            deleteImage(product.ImagePath);
                        }
                    }
                    _productService.UpdateProduct(model.Product);
                    model.Message = "Updated Successfuly";
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
            FillUp(model);
            return View(model);
        }
        [HttpPost]
        [Authorize(Permissions.Products.Delete)]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _productService.Delete(id);
                return Json(new
                {
                    Message = "Item Deleted",
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
        private void FillUp(ProductViewModel model)
        {
            model.CategoriesList = _categoryService.GetCategories()
                    .Select(c => new SelectListItem()
                    {
                        Text = c.Name,
                        Value = c.Id.ToString()
                    }).ToList();
        }
        private void deleteImage(string image)
        {

            var path = new PhysicalFileProvider(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Products")).Root + $@"\{image}";

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}

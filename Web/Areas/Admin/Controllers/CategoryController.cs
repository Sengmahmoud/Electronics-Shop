using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Authorize(Permissions.Categories.Defualt)]
        public IActionResult Index()
        {
            var model = new CategoryViewModel()
            {
                Categories = _categoryService.GetCategories()
            };

            return View(model);
        }
        [Authorize(Permissions.Categories.Add)]
        public IActionResult Create()
        {
            var model = new CategoryViewModel()
            {
                Category = new CategoryInputDto()
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Permissions.Categories.Add)]
        public IActionResult Create(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.Category.Image != null)
                    {
                        var fileName = $"{Guid.NewGuid()}-{model.Category.Image.FileName}";
                        var filepath =
                            new PhysicalFileProvider(
                                    System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Categories"))
                                .Root + $@"\{fileName}";

                        using FileStream fs = System.IO.File.Create(filepath);
                        model.Category.Image.CopyTo(fs);
                        fs.Flush();
                        model.Category.ImagePath = fileName;
                    }

                    _categoryService.AddCategory(model.Category);
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
        [Authorize(Permissions.Categories.Edit)]
        public IActionResult Edit(int id)
        {
            var model = new CategoryViewModel()
            {
                Category = _categoryService.GetCategory(id)
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Permissions.Categories.Edit)]
        public IActionResult Edit(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var category = _categoryService.GetCategory(model.Category.Id);
                    if (model.Category.Image != null)
                    {
                        var fileName = $"{Guid.NewGuid()}-{model.Category.Image.FileName}";
                        var filepath =
                            new PhysicalFileProvider(
                                    System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Categories"))
                                .Root + $@"\{fileName}";

                        using FileStream fs = System.IO.File.Create(filepath);
                        model.Category.Image.CopyTo(fs);
                        fs.Flush();
                        model.Category.ImagePath = fileName;
                        if (category != null && !string.IsNullOrWhiteSpace(category.ImagePath))
                        {
                            deleteImage(category.ImagePath);
                        }
                    }
                    _categoryService.UpdateCategory(model.Category);
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
            return View(model);
        }
        [HttpPost]
        [Authorize(Permissions.Categories.Delete)]
        public IActionResult Delete(int id)
        {
            try
            {
                _categoryService.Delete(id);
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
        private void deleteImage(string image)
        {

            var path = new PhysicalFileProvider(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Categories")).Root + $@"\{image}";

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}

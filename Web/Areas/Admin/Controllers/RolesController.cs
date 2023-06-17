using Core.Common.Exceptions;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly ILogger<RolesController> _logger;
        private readonly ISecurityService _securityService;
        public RolesController(ILogger<RolesController> logger, UserManager<User> userManager,
            RoleManager<Role> roleManager,
         ISecurityService securityService)
        {
            _securityService = securityService;
            _logger = logger;
        }
        public IActionResult Index(int page = 1, int pageSize = 25)
        {
            var model = new RoleViewModel
            {
                Roles = _securityService.GetRoles(page, pageSize)
            };
            return View(model);
        }

        public IActionResult Add()
        {
            var model = new RoleViewModel()
            {
                RoleInput = new RoleInputDto()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _securityService.AddRole(model.RoleInput);
                    model.Success = true;
                    model.Message = "Added Successfuly";

                }
                catch (BusinessValidationException ex)
                {
                    _logger.LogInformation(ex, ex.Message);
                    model.Success = false;
                    model.Message = ex.Message;

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    model.Success = false;
                    model.Message = "Faild";


                }
            }
            return View(model);
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = new RoleViewModel()
            {
                RoleInput = await _securityService.GetRole(id)
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _securityService.EditRole(model.RoleInput);
                    model.Success = true;
                    model.Message = "Added Successfuly";

                }
                catch (BusinessValidationException ex)
                {
                    _logger.LogInformation(ex, ex.Message);
                    model.Success = false;
                    model.Message = "Faild";

                }

                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    model.Success = false;
                    model.Message = "Error";


                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _securityService.RemoveRole(id);
                return Json(new
                {
                    Sucess = true,
                    Message = "Deltetdd Successsfuly"
                });
            }
            catch (BusinessValidationException ex)
            {
                return Json(new
                {

                    Success = false,
                    Message = "Faild"
                });

            }
            catch (Exception)
            {

                return Json(new
                {
                    Sucess = false,
                    Message = "Error"
                });
            }
        }




    }
}


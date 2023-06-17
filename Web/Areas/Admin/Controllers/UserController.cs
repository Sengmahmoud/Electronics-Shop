
using Core.Common.Exceptions;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly ISecurityService _securityService;

        public UserController(ILogger<UserController> logger, UserManager<User> userManager,
           ISecurityService securityService)
        {
            _logger = logger;
            _securityService = securityService;
        }
        [Authorize(Permissions.Users.Defualt)]
        public IActionResult Index(int page = 1, int pageSize = 25)
        {
            var model = new UserViewModel
            {
                Users = _securityService.GetUsers(page, pageSize)
            };
            return View(model);
        }
        public IActionResult Add()
        {
            var model = new UserViewModel()
            {
                User = new UserInputDto()
                {
                    UserRoles = new List<UserRoleDto>()
                }
            };
            FillUp(model);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.User.UserRoles = model.User.UserRoles.Where(r => r != null).ToList();
                 await   _securityService.AddUser(model.User);
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
                    model.Message = "Faild";

                }
            }
            FillUp(model);
            return View(model);
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = new UserViewModel()
            {
                User = await _securityService.GetUser(id)
            };
            model.User.Password = "**";
            model.User.PasswordConfirm = "**";
            FillUp(model);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.User.UserRoles = model.User.UserRoles.Where(r => r != null).ToList();
                    await _securityService.EditUser(model.User);
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
                    model.Message = "Faild";
                }
            }
            model.User = await _securityService.GetUser(model.User.Id);
            FillUp(model);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {

            try
            {

                await _securityService.RemoveUser(id);
                return Json(new
                {
                    Sucess = true,
                    Message = "Deltetdd Successsfuly"
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


        private void FillUp(UserViewModel model)
        {
            model.Roles = _securityService.GetRoles(1, int.MaxValue);
        }
    }
}

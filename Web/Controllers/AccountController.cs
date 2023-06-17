
namespace Web.Controllers
{

  [AllowAnonymous]
        public class AccountController : Controller
        {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISecurityService _securityService;
        const string passwordRex = "";

            public AccountController(UserManager<User> userManager,
                SignInManager<User> signInManager, ILogger<AccountController> logger,
               IHttpContextAccessor httpContextAccessor,ISecurityService securityService)
            {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _securityService = securityService;
        }

            public IActionResult Login(string returnUrl = null)
            {
                ViewData["ReturnUrl"] = returnUrl;
                var model = new LoginViewModel();
                return View(model);
            }

            [HttpPost]
            [AllowAnonymous]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl)
            {
                ViewData["ReturnUrl"] = returnUrl;

                if (ModelState.IsValid)
                {
                    // This doesn't count login failures towards account lockout
                    // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (user == null)
                    {
                        model.Message = "NotSuccessfulLogin";
                        return View(model);
                    }
                

                    await _userManager.UpdateSecurityStampAsync(user);

                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe,
                        lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        _logger.LogInformation(1, "User logged in.");

                        //var user = await UserManager.FindByNameAsync(model.Email);



                        //user.LastLogin = DateTime.Now;
                        var lastLoginResult = await _userManager.UpdateAsync(user);
                        if (!lastLoginResult.Succeeded)
                        {
                            var message = $"Unexpected error occurred setting the last login date" +
                                     $" ({lastLoginResult.ToString()}) for user with ID '{user.Id}'.";
                            _logger.LogError(message);
                        }

                        if (returnUrl != null)
                            return RedirectToLocal(returnUrl);

                        return RedirectToAction("Index", "Home");
                    }

                    if (result.IsLockedOut)
                    {
                        _logger.LogWarning(2, "User account locked out.");
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return View(model);
                    }

                    model.Message = "NotSuccessfulLogin";
                    return View(model);
                }

                return View(model);
            }

        public IActionResult Register()
        {
            var model = new RegisterViewModel()
            {
                User=new UserInputDto()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                try
                {
                    await _securityService.AddUser(model.User);
                   
                    model.Success = true;
                    model.Message = "Registered";
                    if (returnUrl != null)
                        return RedirectToLocal(returnUrl);
                    return RedirectToAction(actionName: "Index", "Home");
                }
              
                catch (Exception ex)
                {
                    model.Message = "GenericError";
                  
                }
            }
            else
            {
                model.Message = "GenericError";
            }
            return View(model);
        }

        public async Task<IActionResult> LogOff()
            {
                await _signInManager.SignOutAsync();
                _logger.LogInformation(4, "User logged out.");
                return RedirectToAction("Login");
            }

            public IActionResult AccessDenied(string ReturnUrl)
            {
                return RedirectToAction("login", new { ReturnUrl = ReturnUrl });
            }

            private IActionResult RedirectToLocal(string returnUrl)
            {
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return null;
                }
            }
        }
    }



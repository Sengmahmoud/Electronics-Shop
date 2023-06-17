using Core.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Components;

public class CurrentUserViewComponent : ViewComponent
{
    private readonly UserManager<User> _userManager;

    public CurrentUserViewComponent(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var user = await _userManager.GetUserAsync(Request.HttpContext.User);

        var model = new CurrentUserViewModel
        {
            WellcomeMessage = "WellcomeMessage",
            Name = user?.Name ?? "",
            Email = user?.Email ?? "",
            UserName = user?.UserName ?? "",
            Logo = ""
        };

        return View(model);
    }

    }

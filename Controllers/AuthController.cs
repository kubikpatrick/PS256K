using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using PS256K.Models.Identity;
using PS256K.Models.REST;

namespace PS256K.Controllers;

[Route("auth")]
public sealed class AuthController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet("login")]
    public ActionResult Login()
    {
        return View();
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user is null)
        {
            ModelState.AddModelError(string.Empty, "Credentials are invalid.");
            
            return View(model);
        }

        var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
        if (!result.Succeeded)
        {
            ModelState.AddModelError(string.Empty, "Credentials are invalid.");

            return View(model);
        }

        return RedirectToAction(nameof(Index), "Home");
    }

    [HttpGet("sign-up")]
    public ActionResult SignUp()
    {
        return View();
    }

    [HttpPost("sign-up")]
    public async Task<ActionResult> SignUp(SignUpModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = new User
        {
            Email = model.Email,
            UserName = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Avatar = "DEFAULT",
            CreatedAt = DateTime.UtcNow,
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            ModelState.AddModelError(string.Empty, "An error occurred while creating your account.");
            
            return View(model);
        }

        await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

        return RedirectToAction("Index", "Home");
    }

    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        if (_signInManager.IsSignedIn(User))
        {
            await _signInManager.SignOutAsync();
        }

        return RedirectToAction(nameof(Login));
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (_signInManager.IsSignedIn(User))
        {
            RedirectToAction("Index", "Home");
        }

        base.OnActionExecuting(context);
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

using PS256K.Data;
using PS256K.Models.Identity;

namespace PS256K.Controllers;

[Authorize]
[Route("account")]
public sealed class AccountController : Controller
{   
    private readonly ApplicationDbContext _context;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public AccountController(ApplicationDbContext context, SignInManager<User> signInManager, UserManager<User> userManager)
    {
        _context = context;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var user = await _context.Users
            .Include(u => u.Albums)
                .ThenInclude(a => a.Pictures) 
            .FirstOrDefaultAsync(u => u.Id == _userManager.GetUserId(User));

        if (user is null)
        {
            return NotFound();
        }

        return View(user);
    }
}
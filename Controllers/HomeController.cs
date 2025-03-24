using System.Diagnostics;
using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using PS256K.Data;
using PS256K.Models;
using PS256K.Models.Identity;

namespace PS256K.Controllers;

[Authorize]
public sealed class HomeController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly SignInManager<User> _signInManager;

    public HomeController(ApplicationDbContext context, SignInManager<User> signInManager)
    {
        _context = context;
        _signInManager = signInManager;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult> Index()
    {
        if (!_signInManager.IsSignedIn(User))
        {
            return View("Presentation");
        }

        var albums = await _context.Albums
            .Include(a => a.Pictures.Take(5))
            .Where(a => a.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
            .ToListAsync();

        return View(albums);
    }

    [HttpGet("privacy")]
    public ActionResult Privacy()
    {
        return View();
    }

    [HttpGet("stack")]
    public ActionResult Stack()
    {
        return View();
    }

    [HttpGet("errors")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public ActionResult Error()
    {
        return View(new ErrorViewModel 
        { 
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier 
        });
    }
}

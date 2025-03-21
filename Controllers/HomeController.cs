using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using PS256K.Data;
using PS256K.Models;

namespace PS256K.Controllers;

public sealed class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var albums = await _context.Albums
            .Include(a => a.Pictures.Take(5))
            .OrderByDescending(a => a.CreatedAt)
            .Where(a => a.IsPublic)
            .Take(20)
            .ToListAsync();

        return View(albums);
    }

    [HttpGet("privacy")]
    public ActionResult Privacy()
    {
        return View();
    }

    [HttpGet("not-found")]
    public ActionResult NotFound()
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

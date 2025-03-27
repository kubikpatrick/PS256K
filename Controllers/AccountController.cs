using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
            .Include(u => u.Projects)
                .ThenInclude(p => p.Pictures)
            .Include(u => u.Connections.OrderByDescending(c => c.CreatedAt).Take(5))
            .FirstOrDefaultAsync(u => u.Id == _userManager.GetUserId(User));

        if (user is null)
        {
            return NotFound();
        }

        return View(user);
    }

    [HttpPost("avatar")]
    public async Task<ActionResult> EditAvatar(IFormFile file)
    {
        if (file is null || file.Length < 0)
        {
            return BadRequest();
        }

        var hash = file.GetHashCode().ToString();
        var directory = Directory.CreateDirectory(Path.Combine("wwwroot", "avatars"));
        var path = Path.Combine(directory.FullName, hash + Path.GetExtension(file.FileName));

        using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
        {
            await file.CopyToAsync(fs);
        }

        int count = await _context.Users
            .Where(u => u.Id == _userManager.GetUserId(User))
            .ExecuteUpdateAsync(u => u.SetProperty(u => u.Avatar, hash + Path.GetExtension(file.FileName)));

        return count > 0 ? Ok() : BadRequest();
    }

    [HttpPost("delete")]
    public async Task<ActionResult> Delete()
    {
        var user = await _userManager.GetUserAsync(User);
        if (_signInManager.IsSignedIn(User))
        {
            await _signInManager.SignOutAsync();
        }

        await _userManager.DeleteAsync(user);

        return RedirectToAction(nameof(Index), "Account");
    }
}
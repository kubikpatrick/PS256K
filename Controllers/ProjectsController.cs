using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using PS256K.Data;
using PS256K.Models.Commerce;
using PS256K.Models.Identity;
using PS256K.Models.REST;

namespace PS256K.Controllers;

[Authorize]
[Route("projects")]
public sealed class ProjectsController : Controller
{   
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;

    public ProjectsController(ApplicationDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var projects = await _context.Projects
            .Include(p => p.Customer)
            .Include(p => p.Pictures)
            .Where(p => p.UserId == _userManager.GetUserId(User))
            .ToListAsync();

        if (projects.Count == 0)
        {
            return RedirectToAction(nameof(Create));
        }

        return View(projects);
    }

    [HttpGet("show/{id:guid}")]
    public async Task<ActionResult> Show([FromRoute] string id)
    {
        var project = await _context.Projects
            .Include(p => p.Customer)
            .Include(p => p.Pictures)
            .FirstOrDefaultAsync(p => p.Id == id);
 
        if (project is null || project.UserId != _userManager.GetUserId(User))
        {
            return NotFound();
        }

        return View(project);
    }

    [HttpGet("create/{customerId:guid}")]
    public async Task<ActionResult> Create([FromRoute] string customerId)
    {
        bool allowed = await _context.Customers.AnyAsync(c => c.Id == customerId && c.UserId == _userManager.GetUserId(User));
        if (!allowed)
        {
            return NotFound();
        }

        return View();
    }

    [HttpPost("create/{customerId:guid}")]
    public async Task<ActionResult> Create([FromRoute] string customerId, [FromForm] ProjectModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        bool allowed = await _context.Customers.AnyAsync(c => c.Id == customerId && c.UserId == _userManager.GetUserId(User));
        if (!allowed)
        {
            return NotFound();
        }

        var entry = await _context.Projects.AddAsync(new Project
        {
            Name = model.Name,
            CreatedAt = DateTime.Now,
            CustomerId = customerId,
            UserId = _userManager.GetUserId(User),
        });

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Show), new 
        {
            id = entry.Entity.Id
        });
    }

    [HttpDelete("delete/{id:guid}")]
    public async Task<ActionResult> Delete([FromRoute] string id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project is null || project.UserId != _userManager.GetUserId(User))
        {
            return NotFound();
        }

        string path = Path.Combine("wwwroot", "uploads", project.Id);

        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }

        _context.Projects.Remove(project);

        await _context.SaveChangesAsync();

        return Ok();
    }
}
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
[Route("customers")]
public sealed class CustomersController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;

    public CustomersController(ApplicationDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var customers = await _context.Customers
            .Include(c => c.Projects)
            .Where(c => c.UserId == _userManager.GetUserId(User))
            .ToListAsync();

        if (customers.Count == 0)
        {
            return RedirectToAction(nameof(Create));
        }

        return View(customers);
    }

    [HttpGet("show/{id:guid}")]
    public async Task<ActionResult> Show([FromRoute] string id)
    {
        var customer = await _context.Customers
            .Include(c => c.Projects)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (customer is null || customer.UserId != _userManager.GetUserId(User))
        {
            return NotFound();
        }

        return View(customer);
    }

    [HttpGet("search")]
    public async Task<ActionResult> Search([FromQuery] string name)
    {
        name = name.ToLower();

        var customers = await _context.Customers
            .Where(
                c => c.UserId == _userManager.GetUserId(User) &&
                (
                    c.FirstName.ToLower().Contains(name) || 
                    c.LastName.ToLower().Contains(name)
                )
            )
            .ToListAsync();

        return View(customers);
    }

    [HttpGet("create")]
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost("create")]
    public async Task<ActionResult> Create([FromForm] CustomerModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        bool exists = await _context.Customers.AnyAsync(c => c.Email == model.Email);
        if (exists)
        {
            ModelState.AddModelError(string.Empty, "This client arelady exists.");

            return View(model);
        }
        
        if (model.File is not null && model.File.Length > 0)
        {
            var hash = model.File.GetHashCode().ToString();
            var directory = Directory.CreateDirectory(Path.Combine("wwwroot", "uploads", model.FirstName));
            var path = Path.Combine(directory.FullName, hash + Path.GetExtension(model.File.FileName));
            
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                await model.File.CopyToAsync(fs);
            }
        }

        var entry = await _context.Customers.AddAsync(new Customer
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Phone = model.Phone,
            Occupation = model.Occupation,
            CreatedAt = DateTime.UtcNow,
            UserId = _userManager.GetUserId(User)
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
        var customer = await _context.Customers.FindAsync(id);
        if (customer is null || customer.UserId != _userManager.GetUserId(User))
        {
            return NotFound();
        }

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}
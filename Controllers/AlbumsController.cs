using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using PS256K.Data;
using PS256K.Models.Gallery;
using PS256K.Models.REST;

namespace PS256K.Controllers;

[Authorize]
[Route("albums")]
public sealed class AlbumsController : Controller
{
    private readonly ApplicationDbContext _context;

    public AlbumsController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var albums = await _context.Albums
            .Include(a => a.Pictures.Take(5))
            .Where(a => a.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
            .ToListAsync();

        return View(albums);
    }

    [AllowAnonymous]
    [HttpGet("show/{id:guid}")]
    public async Task<ActionResult> Show([FromRoute] string id)
    {
        var album = await _context.Albums
            .Include(a => a.Pictures)
            .Include(a => a.User)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (album is null || (album.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier) && !album.IsPublic))
        {
            return NotFound();
        }

        return View(album);
    }

    [HttpGet("search")]
    public async Task<ActionResult> Search([FromQuery] string name)
    {
        var albums = await _context.Albums
            .Include(a => a.Pictures)
            .Where(
                a => a.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier) &&
                a.Name.Contains(name)
            )
            .ToListAsync();

        return View(albums);
    }

    [HttpGet("create")]
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost("create")]
    public async Task<ActionResult> Create([FromForm] AlbumModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var entry = await _context.Albums.AddAsync(new Album
        {
            Name = model.Name,
            IsPublic = model.IsPublic,
            CreatedAt = DateTime.Now,
            UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
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
        var album = await _context.Albums.FindAsync(id);
        if (album is null || album.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
        {
            return NotFound();
        }

        _context.Albums.Remove(album);

        await _context.SaveChangesAsync();

        return Ok();
    }
}
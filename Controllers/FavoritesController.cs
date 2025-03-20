using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using PS256K.Data;
using PS256K.Models.Gallery;

namespace PS256K.Controllers;

[Authorize]
[Route("favorites")]
public sealed class FavoritesController : Controller
{
    private readonly ApplicationDbContext _context;

    public FavoritesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("create/{id:guid}")]
    public async Task<ActionResult> Create([FromRoute] string id)
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        bool exists = await _context.Albums
            .AnyAsync(
                a => a.Id == id &&
                a.UserId == userId
            );

        if (!exists)
        {
            return NotFound();
        }

        await _context.Favorites.AddAsync(new Favorite
        {
            AlbumId = id,
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
        });

        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("delete/{id:guid}")]
    public async Task<ActionResult> Delete([FromRoute] string id)
    {
        bool exists = await _context.Favorites
            .AnyAsync(
            a => a.Id == id &&
                a.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier)
            );

        if (!exists)
        {
            return NotFound();
        }

        await _context.Favorites.Where(f => f.Id == id).ExecuteDeleteAsync();

        return Ok();
    }
}

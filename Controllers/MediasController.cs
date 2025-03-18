using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using PS256K.Data;
using PS256K.Helpers;
using PS256K.Models.Gallery;

namespace PS256K.Controllers;

[Authorize]
[Route("medias")]
public sealed class MediasController : Controller
{
    private readonly ApplicationDbContext _context;

    public MediasController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("create/{albumId}")]
    public async Task<ActionResult> Create([FromRoute] string albumId, IFormFile file)
    {   
        if (file is null || file.Length == 0)
        {
            return BadRequest();
        }

        var hash = file.GetHashCode().ToString();
        var directory = Directory.CreateDirectory(Path.Combine("wwwroot", "uploads", albumId));
        var path = Path.Combine(directory.FullName, hash + Path.GetExtension(file.FileName));

        using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
        {
            await file.CopyToAsync(fs);
        }

        await _context.Medias.AddAsync(new Media
        {
            Name = Path.GetFileName(file.FileName),
            Hash = hash + Path.GetExtension(file.FileName),
            AlbumId = albumId,
        });

        await _context.SaveChangesAsync();

        return RedirectToAction("Show", "Albums", new
        {
            albumId
        });
    }

    [HttpDelete("delete/{id:guid}")]
    public async Task<ActionResult> Delete([FromRoute] string id)
    {
        var media = await _context.Medias
            .FirstOrDefaultAsync(
                m => m.Id == id && 
                m.Album.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier)
            );

        if (media is null)
        {
            return NotFound();
        }

        FileHelper.Delete(media.Hash);
        _context.Medias.Remove(media);

        await _context.SaveChangesAsync();

        return Ok();         
    }
}
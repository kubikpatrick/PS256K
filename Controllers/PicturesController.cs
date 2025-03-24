using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using PS256K.Data;
using PS256K.Helpers;
using PS256K.Models.Gallery;

namespace PS256K.Controllers;

[Authorize]
[Route("pictures")]
public sealed class PicturesController : Controller
{
    private readonly ApplicationDbContext _context;

    public PicturesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("create/{albumId}")]
    public async Task<ActionResult> Create([FromRoute] string albumId, List<IFormFile> files)
    {   
        if (files is null || files.Count == 0)
        {
            return BadRequest();
        }
        
        foreach (var file in files)
        {
            var hash = file.GetHashCode().ToString();
            var directory = Directory.CreateDirectory(Path.Combine("wwwroot", "uploads", albumId));
            var path = Path.Combine(directory.FullName, hash + Path.GetExtension(file.FileName));

            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                await file.CopyToAsync(fs);
            }

            await _context.Pictures.AddAsync(new Picture
            {
                Name = Path.GetFileName(file.FileName),
                Path = hash + Path.GetExtension(file.FileName),
                AlbumId = albumId,
            });
        }

        await _context.SaveChangesAsync();

        return RedirectToAction("Show", "Albums", new
        {
            id = albumId,
        });
    }

    [HttpDelete("delete/{id:guid}")]
    public async Task<ActionResult> Delete([FromRoute] string id)
    {
        var picture = await _context.Pictures
            .FirstOrDefaultAsync(
                m => m.Id == id && 
                m.Album.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier)
            );

        if (picture is null)
        {
            return NotFound();
        }

        FileHelper.Delete(picture.Path);
        _context.Pictures.Remove(picture);

        await _context.SaveChangesAsync();

        return Ok();
    }
}
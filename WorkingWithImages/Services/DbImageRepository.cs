using Microsoft.EntityFrameworkCore;
using WorkingWithImages.Models.Entities;

namespace WorkingWithImages.Services;

public class DbImageRepository : IImageRepository
{
    private readonly ApplicationDbContext _db;

    public DbImageRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task DeleteAsync(int id)
    {
        var image = await ReadAsync(id);
        if(image != null)
        {
            _db.Images.Remove(image);
            await _db.SaveChangesAsync();
        }
    }

    public async Task<ICollection<Image>> ReadAllAsync()
    {
        return await _db.Images.ToListAsync();
    }

    public async Task<Image?> ReadAsync(int id)
    {
        return await _db.Images.FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<Image> UploadAsync(IFormFile uploadedImage, string? name = null)
    {
        MemoryStream ms = new();
        await uploadedImage.OpenReadStream().CopyToAsync(ms);
        name ??= uploadedImage.Name;

        Image imageEntity = new()
        {
            Id = 0,
            Name = name,
            Length = uploadedImage.Length,
            Data = ms.ToArray(),
            ContentType = uploadedImage.ContentType
        };

        _db.Images.Add(imageEntity);

        await _db.SaveChangesAsync();
        return imageEntity;
    }
}

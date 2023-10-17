using WorkingWithImages.Models.Entities;

namespace WorkingWithImages.Services;

public interface IImageRepository
{
    Task<Image> UploadAsync(IFormFile uploadedImage, string? name = null);
    Task<Image?> ReadAsync(int id);
    Task<ICollection<Image>> ReadAllAsync();
    Task DeleteAsync(int id);
}


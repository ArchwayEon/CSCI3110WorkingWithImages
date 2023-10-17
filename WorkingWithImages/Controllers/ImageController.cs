using Microsoft.AspNetCore.Mvc;
using WorkingWithImages.Models.Entities;
using WorkingWithImages.Services;

namespace WorkingWithImages.Controllers;

public class ImageController : Controller
{
    private readonly IImageRepository _imageRepo;

    public ImageController(IImageRepository imageRepo)
    {
        _imageRepo = imageRepo;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _imageRepo.ReadAllAsync());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Upload(string name, IList<IFormFile> files)
    {
        await TryUploadImage(files, name);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Data(int id)
    {
        var image = await _imageRepo.ReadAsync(id);
        if (image == null || image.Data == null)
        {
            return NotFound();
        }
        MemoryStream ms = new MemoryStream(image.Data);
        return await Task.Run(() => new FileStreamResult(ms, image.ContentType));
    }

    public async Task<IActionResult> Details(int id)
    {
        var image = await _imageRepo.ReadAsync(id);
        if (image == null || image.Data == null)
        {
            return RedirectToAction("Index");
        }
        return View(image);
    }

    protected async Task<Image?> TryUploadImage(IList<IFormFile> files, string? name = null)
    {
        Image? image = null;
        IFormFile? uploadedImage = files.FirstOrDefault();
        if (uploadedImage != null && uploadedImage.ContentType.ToLower().StartsWith("image/"))
        {
            image = await _imageRepo.UploadAsync(uploadedImage, name);
        }
        return image;
    }

}

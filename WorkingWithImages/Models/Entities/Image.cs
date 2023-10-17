namespace WorkingWithImages.Models.Entities;

public class Image
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public byte[]? Data { get; set; }
    public long Length { get; set; }
    public string ContentType { get; set; } = String.Empty;
}


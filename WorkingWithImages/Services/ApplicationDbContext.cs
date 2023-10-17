using Microsoft.EntityFrameworkCore;
using System;
using WorkingWithImages.Models.Entities;

namespace WorkingWithImages.Services;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Image> Images => Set<Image>();
}

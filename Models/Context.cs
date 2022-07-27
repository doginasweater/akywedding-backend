namespace akywedding_backend.Models;

using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

public class WeddingContext : DbContext {
  public WeddingContext(DbContextOptions<WeddingContext> options) : base(options) { }

  public DbSet<Guest> guests { get; set; } = null!;
  public DbSet<Meal> meals { get; set; } = null!;
  public DbSet<MealOption> mealOptions { get; set; } = null!;
  public DbSet<Reception> receptions { get; set; } = null!;
}
namespace akywedding_backend.Models.Database;

using Microsoft.EntityFrameworkCore;

public class WeddingContext : DbContext {
  public WeddingContext(DbContextOptions<WeddingContext> options) : base(options) { }

  public DbSet<Guest> guests { get; set; } = null!;
  public DbSet<MealOption> mealOptions { get; set; } = null!;
  public DbSet<Party> parties { get; set; } = null!;
  public DbSet<Rsvp> rsvps { get; set; } = null!;
}
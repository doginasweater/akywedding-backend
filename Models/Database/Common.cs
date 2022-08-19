namespace akywedding_backend.Models.Database;

public class Common {
  public DateTime created_at { get; set; } = DateTime.UtcNow;
  public DateTime? updated_at { get; set; }
}
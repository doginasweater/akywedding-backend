namespace akywedding_backend.Models.ViewModels;

public class GuestViewModel {
  public long guest_id { get; set; }
  public string name { get; set; } = "";
  public bool is_child { get; set; }
  public bool is_attending { get; set; }
  public long meal_id { get; set; }
  public string dietary_restrictions { get; set; } = "";
  public DateTime? created_at { get; set; }
  public DateTime? updated_at { get; set; }
}
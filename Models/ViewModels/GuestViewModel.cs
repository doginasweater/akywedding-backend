using akywedding_backend.Models.Database;

namespace akywedding_backend.Models.ViewModels;

public class GuestViewModel {
  public long guest_id { get; set; }
  public string name { get; set; } = "";
  public bool is_attending { get; set; }
  public MealOption? meal { get; set; }
  public string dietary_restrictions { get; set; } = "";
}
namespace akywedding_backend.Models.Database;

public class Guest {
  public long id { get; set; }
  public string name { get; set; } = "";
  public bool? is_attending { get; set; }
  public MealOption? meal_choice { get; set; }
  public string? dietary_restrictions { get; set; }
}
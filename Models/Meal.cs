namespace akywedding_backend.Models;

public class Meal {
  public long id { get; set; }
  public MealOption choice { get; set; } = new();
  public string dietaryRestrictions { get; set; } = "";
}
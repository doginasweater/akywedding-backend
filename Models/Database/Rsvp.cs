namespace akywedding_backend.Models.Database;

public class Rsvp {
  public long id { get; set; }
  public string music { get; set; } = "";
  public string comments { get; set; } = "";
  public virtual Party party { get; set; } = new();
}
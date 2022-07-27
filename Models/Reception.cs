namespace akywedding_backend.Models;

public class Reception {
  public long id { get; set; }
  public string music { get; set; } = "";
  public string comments { get; set; } = "";
  public virtual Guest guest { get; set; } = new();
}
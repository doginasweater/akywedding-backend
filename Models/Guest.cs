namespace akywedding_backend.Models;

public class Guest {
  public long id { get; set; }
  public string name { get; set; } = "";
  public bool isAttending { get; set; }
}
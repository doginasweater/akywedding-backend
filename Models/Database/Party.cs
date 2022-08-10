namespace akywedding_backend.Models.Database;

public class Party {
  public long id { get; set; }
  public string name { get; set; } = "";
  public virtual ICollection<Guest> guests { get; set; } = new HashSet<Guest>();
}
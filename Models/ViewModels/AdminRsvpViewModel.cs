namespace akywedding_backend.Models.ViewModels;

public class AdminRsvpViewModel {
  public long rsvp_id { get; set; }
  public string music { get; set; } = "";
  public string comments { get; set; } = "";
  public IEnumerable<GuestViewModel> guests { get; set; } = new List<GuestViewModel>();
  public DateTime created_at { get; set; }
  public DateTime? updated_at { get; set; }
}
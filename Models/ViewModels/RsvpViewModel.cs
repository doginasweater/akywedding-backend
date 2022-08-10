namespace akywedding_backend.Models.ViewModels;

public class RsvpViewModel {
  public long partyId { get; set; }
  public IEnumerable<GuestViewModel> guests { get; set; } = new List<GuestViewModel>();
  public string music { get; set; } = "";
  public string comments { get; set; } = "";
}


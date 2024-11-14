namespace domain.entities;

public partial class SearchHistory
{
    public int SearchId { get; set; }

    public int? UserId { get; set; }

    public string? SearchQuery { get; set; }

    public DateTime? SearchedDate { get; set; }

    public virtual User? User { get; set; }
}

namespace domain.entities;

public partial class ClubAdditionalInfo
{
    public int? ClubId { get; set; }

    public DateTime? ClubCreatedDate { get; set; }

    public string? ClubDescription { get; set; }

    public virtual Club? Club { get; set; }

    public static ClubAdditionalInfo CreateDefault(Club club)
    {
        return new ClubAdditionalInfo
        {
            ClubId = club.ClubId,
            ClubCreatedDate = DateTime.Now
        };
    }
}

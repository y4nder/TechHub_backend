namespace domain.entities;

public partial class ClubCategory
{
    public int ClubCategoryId { get; set; }

    public string ClubCategoryName { get; set; } = null!;

    public virtual ICollection<Club> Clubs { get; set; } = new List<Club>();
    
}

public class ClubCategoryDto
{
    public int ClubCategoryId { get; set; }
    public string ClubCategoryName { get; set; } = null!;
}

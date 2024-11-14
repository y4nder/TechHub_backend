namespace domain.entities;

public partial class Tag
{
    public int TagId { get; set; }

    public string TagName { get; set; } = null!; 

    public int TagCount { get; set; } = 0;

    public virtual ICollection<UserTagFollow> UserTagFollows { get; set; } = new List<UserTagFollow>();

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
}

namespace domain.entities;

public partial class Tag
{
    public int TagId { get; set; }

    public string TagName { get; set; } = null!; 

    public int TagCount { get; set; } = 0;

    public string TagDescription { get; set; } = String.Empty;
    public virtual ICollection<UserTagFollow> UserTagFollows { get; set; } = new List<UserTagFollow>();

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
}

public class TagDto
{
    public int TagId { get; set; }
    public string TagName { get; set; } = null!;
}

public class GroupedTagList
{
    public string Group { get; set; } = null!;
    public List<TagDto> Tags { get; set; } = new();
}

public class TrendingTagDto
{
    public int TagId { get; set; }
    public string TagName { get; set; } = null!;
    public int TagCount { get; set; }
}

public class TagPageDto
{
    public int TagId { get; set; }
    public string TagName { get; set; } = null!;
    public bool Followed { get; set; }
    public string TagDescription { get; set; } = null!;
}
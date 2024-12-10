using domain.entities;

namespace domain.interfaces;

public interface IArticleBookmarkRepository
{
    void AddBookmark(UserArticleBookmark bookmark);
    
    Task<bool> BookmarkExist(UserArticleBookmark bookmark);
    
    Task RemoveBookmark(UserArticleBookmark bookmark);
}
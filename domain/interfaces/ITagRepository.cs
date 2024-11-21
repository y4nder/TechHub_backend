using domain.entities;

namespace domain.interfaces;

public interface ITagRepository
{
    Task<bool> AreAllIdsValidAsync(List<int> tagIds);
    
    Task<bool> TagIdExistsAsync(int tagId);
    Task<List<Tag>> GetTagsManyAsync(List<int> requestTagIds);
    void BatchTagUpdate(ICollection<Tag> tags);
    Task<List<TagDto>> GetTagsByQueryAsync(string tagSearchQuery);
    Task<bool> AreNewTagsUniqueAsync(List<string> newTags);
    
    void BatchAddTags(List<Tag> tags);
}
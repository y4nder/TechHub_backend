namespace domain.interfaces;

public interface ITagRepository
{
    Task<bool> AreAllIdsValidAsync(List<int> tagIds);
    
    Task<bool> TagIdExistsAsync(int tagId);
}
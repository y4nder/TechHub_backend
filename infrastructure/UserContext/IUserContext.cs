namespace infrastructure.UserContext;

public interface IUserContext
{
    int GetUserId();

    string GetUserIdAsString();
}

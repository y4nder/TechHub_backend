using System.Net;

namespace application.Exceptions.FollowInteractionExceptions;

public class FollowInteractionException : HttpRequestException
{
    private FollowInteractionException(string? message, Exception? inner, HttpStatusCode? statusCode) : base(message, inner, statusCode)
    {
    }

    public static FollowInteractionException CannotFollowSelf()
    {
        return new FollowInteractionException("Cannot follow self", null, HttpStatusCode.Conflict);
    }

    public static FollowInteractionException FollowingIsNotFound()
    {
        return new FollowInteractionException("Following is not found", null, HttpStatusCode.NotFound);
    }

    public static FollowInteractionException FollowerIsNotFound()
    {
        return new FollowInteractionException("Follower is not found", null, HttpStatusCode.NotFound);
    }

    public static FollowInteractionException AlreadyFollowed()
    {
        return new FollowInteractionException("Already followed", null, HttpStatusCode.Conflict);
    }

    public static FollowInteractionException CannotUnfollowSelf()
    {
        return new FollowInteractionException("Cannot unfollow self", null, HttpStatusCode.Conflict);
    }

    public static Exception NotFollowed()
    {
        return new FollowInteractionException("Not followed", null, HttpStatusCode.NotFound);
    }
}
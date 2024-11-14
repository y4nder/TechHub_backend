using System.Net;

namespace application.Exceptions.TagExceptions;

public class TagException : HttpRequestException
{
    private TagException(string? message, Exception? inner, HttpStatusCode? statusCode) : base(message, inner, statusCode)
    {
    }

    public static TagException UserIdNotFound(int id)
    {
        return new TagException($"User {id} not found", null, HttpStatusCode.NotFound);
    }

    public static TagException ZeroFollowMany()
    {
        return new TagException("Must follow at least one tag", null, HttpStatusCode.BadRequest);
    }

    public static TagException DuplicateTags()
    {
        return new TagException("Duplicate tags", null, HttpStatusCode.BadRequest);
    }
    
    public static TagException InvalidTagIds()
    {
        return new TagException("Invalid tag ids", null, HttpStatusCode.BadRequest);
    }

    public static Exception AlreadyFollowed()
    {
        return new TagException("Tag Already Followed", null, HttpStatusCode.BadRequest);
    }

    public static Exception TagIdNotFound(int requestTagId)
    {
        return new TagException($"Tag {requestTagId} not found", null, HttpStatusCode.NotFound);
    }

    public static Exception NotFollowed()
    {
        return new TagException("Tag Not Followed", null, HttpStatusCode.BadRequest);

    }
}
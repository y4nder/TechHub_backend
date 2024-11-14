using System.Net;

namespace infrastructure.Exceptions;

public class ImageUploadException : HttpRequestException
{
    private ImageUploadException(string? message, Exception? inner, HttpStatusCode? statusCode) : base(message, inner, statusCode)
    {
    }
    
    public static ImageUploadException NoImageFile()
    {
        return new ImageUploadException("No image file selected.",  null, HttpStatusCode.BadRequest);
    }

    public static ImageUploadException ImageFileTooLarge(long maxSizeInBytes)
    {
        return new ImageUploadException($"Image size cannot exceed {maxSizeInBytes / (1024 * 1024)} MB.",  null, HttpStatusCode.BadRequest);
    }

    public static ImageUploadException ImageUploadFailed()
    {
        return new ImageUploadException("Image upload failed.",  null, HttpStatusCode.BadRequest);
    }
    
    public static ImageUploadException ImageUploadFailed(Exception inner)
    {
        return new ImageUploadException(inner.Message,  inner, HttpStatusCode.BadRequest);
    }

    public static ImageUploadException ImageIdNotFound(string imageId)
    {
        return new ImageUploadException($"Image id: {imageId} not found.",  null, HttpStatusCode.NotFound);
    }
}
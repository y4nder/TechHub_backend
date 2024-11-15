using infrastructure.services.cloudinary;
using Microsoft.AspNetCore.Http;

namespace application.utilities.ImageUploads.Article;

public interface IArticleImageService
{
    Task<ImageUploadResponse?> UploadImage(IFormFile file);
}
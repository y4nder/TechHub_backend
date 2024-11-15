using infrastructure.services.cloudinary;
using Microsoft.AspNetCore.Http;

namespace application.utilities.ImageUploads.Article;

public class ArticleImageService : IArticleImageService
{
    public Task<ImageUploadResponse?> UploadImage(IFormFile file)
    {
        throw new NotImplementedException();
    }
}
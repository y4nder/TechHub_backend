using infrastructure.services.cloudinary;
using Microsoft.AspNetCore.Http;

namespace application.utilities.ImageUploads.Article;

public class ArticleImageService : IArticleImageService
{
    private readonly IImageUploader _imageUploader;
    private const string FolderName = "article_images/thumbnails";
    public ArticleImageService(IImageUploader imageUploader)
    {
        _imageUploader = imageUploader;
    }

    public async Task<ImageUploadResponse?> UploadThumbnail(IFormFile file)
    {
        return await _imageUploader.UploadImageAsync(file, FolderName);
    }
}
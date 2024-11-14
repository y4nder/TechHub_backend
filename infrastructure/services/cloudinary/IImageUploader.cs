using Microsoft.AspNetCore.Http;

namespace infrastructure.services.cloudinary;

public interface IImageUploader
{
    Task<ImageUploadResponse> UploadImageAsync(IFormFile imageFile, string folderName, long maxSizeInBytes = 2 * 1024 * 1024);

    Task<bool> DeleteImageAsync(string publicId);
}
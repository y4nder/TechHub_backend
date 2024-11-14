using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace infrastructure.services.cloudinary;

public class ImageUploader : IImageUploader
{
    private readonly Cloudinary _cloudinary;

    public ImageUploader(IOptions<CloudinarySettings> settings)
    {
        var cloudinarySettings = settings.Value;
        
        _cloudinary = new Cloudinary(
            new Account(
            cloudinarySettings.KeyName,
            cloudinarySettings.ApiKey,
            cloudinarySettings.ApiSecret
            )
        );
    }
    
    public async Task<ImageUploadResponse> UploadImageAsync(IFormFile imageFile, string folderName, long maxSizeInBytes = 2097152)
    {
        if (imageFile == null || imageFile.Length == 0)
        {
            throw ImageUploadException.NoImageFile();
        }

        // File size validation (e.g., limit to 2MB)
        if (imageFile.Length > maxSizeInBytes)
        {
            //throw new ArgumentException($"Image size cannot exceed {maxSizeInBytes / (1024 * 1024)} MB.");
            
            throw ImageUploadException.ImageFileTooLarge(maxSizeInBytes);
        }


        using (var stream = new MemoryStream())
        {
            await imageFile.CopyToAsync(stream);
            stream.Position = 0;

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(imageFile.FileName, stream),
                AssetFolder = folderName
            };

            try
            {
                // Upload image to Cloudinary
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return new ImageUploadResponse
                    {
                        ImageSecureUrl = uploadResult.SecureUrl.ToString(),
                        PublicId = uploadResult.PublicId,
                        Format = uploadResult.Format
                    };
                }
                else
                {
                    throw ImageUploadException.ImageUploadFailed();
                }
            }
            catch (Exception ex)
            {
                throw ImageUploadException.ImageUploadFailed(ex);
            }
        }
    }

    public async Task<bool> DeleteImageAsync(string publicId)
    {
        if (string.IsNullOrWhiteSpace(publicId))
            throw ImageUploadException.ImageIdNotFound(publicId);

        var deletionParams = new DeletionParams(publicId);

        var result = await _cloudinary.DestroyAsync(deletionParams);

        return result.Result == "ok";
    }
}

public class ImageUploadResponse
{
    public string ImageSecureUrl { get; set; } = null!;
    public string PublicId { get; set; } = null!;
    public string Format { get; set; } = null!;
}
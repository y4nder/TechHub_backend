using infrastructure.services.cloudinary;
using Microsoft.AspNetCore.Http;

namespace application.utilities.ImageUploads.Club;

public class ClubImageService : IClubImageService
{
    private readonly IImageUploader _imageUploader;

    public ClubImageService(IImageUploader imageUploader)
    {
        _imageUploader = imageUploader;
    }

    public async Task<ImageUploadResponse> UploadClubProfileImage(IFormFile imageFile)
    {
        return await _imageUploader.UploadImageAsync(imageFile, "club_profiles"); 
    }
}
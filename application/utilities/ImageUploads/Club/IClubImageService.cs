using infrastructure.services.cloudinary;
using Microsoft.AspNetCore.Http;

namespace application.utilities.ImageUploads.Club;

public interface IClubImageService
{
    Task<ImageUploadResponse> UploadClubProfileImage(IFormFile imageFile);
}
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace application.useCases.clubInteractions.CreateClub;

public class CreateClubValidator : AbstractValidator<CreateClubCommand>
{
    public CreateClubValidator()
    {
        RuleFor(c => c.ClubThumbnail)
            .Must(BeAValidSize).WithMessage("Thumbnail must be less than 2 MB.")
            .Must(HaveValidExtension).WithMessage("Thumbnail must be a PNG or JPEG image.");
        
        RuleFor(c => c.ClubName)
            .NotEmpty().WithMessage("Club name is required.")
            .MinimumLength(2).MaximumLength(20).WithMessage("Club name must be between 2 and 20 characters.");
        
    }
    
    private bool BeAValidSize(IFormFile? file)
    {
        const long maxFileSize = 2 * 1024 * 1024; // 2 MB
        if(file is null)
            throw new NullReferenceException("File is null.");
        return file.Length > 0 && file.Length <= maxFileSize;
    }

    private bool HaveValidExtension(IFormFile file)
    {
        var allowedExtensions = new[] { ".png", ".jpeg", ".jpg" };
        var fileExtension = System.IO.Path.GetExtension(file.FileName)?.ToLower();
        return allowedExtensions.Contains(fileExtension);
    }
}
using FluentValidation;
using infrastructure.services.cloudinary;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace application.utilities.ImageUploads;

public class ImageUploadCommand : IRequest<ImageUploadResponse>
{
    public string Folder { get; set; } = null!;
    public IFormFile? Image { get; set; }
}

public class ImageUploadCommandValidator : AbstractValidator<ImageUploadCommand>
{
    public ImageUploadCommandValidator()
    {
        RuleFor(command => command.Folder).NotEmpty().NotNull().WithMessage("Please specify a valid folder.");
        RuleFor(command => command.Image)
            .Must(BeAValidSize).WithMessage("Article thumbnail must be a valid size")
            .Must(HaveValidExtension!).WithMessage("Article thumbnail must be a valid extension");
    }
    
    private bool BeAValidSize(IFormFile? file)
    {
        const long maxFileSize = 2 * 1024 * 1024; // 2 MB
        if(file is null)
            throw new NullReferenceException("File is null.");
        return file.Length is > 0 and <= maxFileSize;
    }
    
    private bool HaveValidExtension(IFormFile file)
    {
        var allowedExtensions = new[] { ".png", ".jpeg", ".jpg" };
        var fileExtension = System.IO.Path.GetExtension(file.FileName)?.ToLower();
        return allowedExtensions.Contains(fileExtension);
    }
}

public class ImageUploadCommandHandler : IRequestHandler<ImageUploadCommand, ImageUploadResponse>
{
    private readonly IImageUploader _imageUploader;
    private readonly IValidator<ImageUploadCommand> _validator;

    public ImageUploadCommandHandler(IImageUploader imageUploader, IValidator<ImageUploadCommand> validator)
    {
        _imageUploader = imageUploader;
        _validator = validator;
    }

    public async Task<ImageUploadResponse> Handle(ImageUploadCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        return await _imageUploader.UploadImageAsync(request.Image!, request.Folder); 
    }
}


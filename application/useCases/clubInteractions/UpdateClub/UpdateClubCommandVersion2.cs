using application.utilities.ImageUploads.Club;
using domain.interfaces;
using FluentValidation;
using infrastructure.Exceptions;
using infrastructure.services.worker;
using infrastructure.UserContext;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace application.useCases.clubInteractions.UpdateClub;

public class UpdateClubCommandVersion2 : IRequest<UpdateClubCommandVersion2Response>
{
    public int ClubId { get; set; }
    public IFormFile? ClubThumbnail { get; set; } = null!;
    public string ClubThumbnailUrl { get; set; } = null!;
    public string ClubName { get; set; } = null!;
    public string? ClubIntroduction { get; set; } = null!;
    public int? ClubCategoryId { get; set; }
    public short? PostPermission { get; set; } = -1;

    public short? InvitePermission { get; set; } = -1;
    public bool IsPrivate { get; set; }
}

public class UpdateClubCommandVersion2Response
{
    public string Message { get; set; } = null!;
}

public class UpdateClubCommandValidator : AbstractValidator<UpdateClubCommandVersion2>
{
    public UpdateClubCommandValidator()
    {
        RuleFor(c => c.ClubThumbnail)
            .Must(BeAValidSize).WithMessage("Thumbnail must be less than 2 MB.")
            .Must(HaveValidExtension!).WithMessage("Thumbnail must be a PNG or JPEG image.")
            .When(c => c.ClubThumbnail is not null);
        
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

public class UpdateClubCommandHandler : IRequestHandler<UpdateClubCommandVersion2, UpdateClubCommandVersion2Response>
{
    private readonly IValidator<UpdateClubCommandVersion2> _validator;
    private readonly IUserContext _userContext;
    private readonly IClubRepository _clubRepository;
    private readonly IClubImageService _clubImageService;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateClubCommandHandler(IUserContext userContext, IClubRepository clubRepository, IValidator<UpdateClubCommandVersion2> validator, IClubImageService clubImageService, IUnitOfWork unitOfWork)
    {
        _userContext = userContext;
        _clubRepository = clubRepository;
        _validator = validator;
        _clubImageService = clubImageService;
        _unitOfWork = unitOfWork;
    }

    public async Task<UpdateClubCommandVersion2Response> Handle(UpdateClubCommandVersion2 request, CancellationToken cancellationToken)
    {
        var userId = _userContext.GetUserId();
        
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if(!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var club = await _clubRepository.GetClubByIdNo(request.ClubId);

        string clubProfilePicUrl = "";

        try
        {
            var clubProfileResponse = await _clubImageService.UploadClubProfileImage(request.ClubThumbnail!)??
                                      throw ImageUploadException.ImageUploadFailed();

            clubProfilePicUrl = clubProfileResponse.ImageSecureUrl;
        }
        catch (Exception ex)
        {
            clubProfilePicUrl = club!.ClubImageUrl!;
        }
        
        club!.ClubImageUrl = clubProfilePicUrl;
        
        club.ClubName = request.ClubName;
        club.ClubIntroduction = request.ClubIntroduction;
        club.ClubCategoryId = request.ClubCategoryId;
        club.PostPermission = request.PostPermission;
        club.InvitePermission = request.InvitePermission;
        club.Private = request.IsPrivate;

        try
        {
            await _unitOfWork.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Error updating club.", ex);
        }

        return new UpdateClubCommandVersion2Response
        {
            Message = "Updated Club",
        };
    }
}
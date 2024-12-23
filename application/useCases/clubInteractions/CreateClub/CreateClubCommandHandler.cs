﻿using application.Events.ClubEvents;
using application.Exceptions.ClubExceptions;
using application.utilities.ImageUploads.Club;
using domain.entities;
using domain.interfaces;
using FluentValidation;
using infrastructure.services.worker;
using infrastructure.UserContext;
using MediatR;

namespace application.useCases.clubInteractions.CreateClub;

public class CreateClubCommandHandler : IRequestHandler<CreateClubCommand, CreateClubResponse>
{
    private readonly IValidator<CreateClubCommand> _validator;
    private readonly IUserContext _userContext;
    private readonly IClubCategoryRepository _clubCategoryRepository;
    private readonly IClubRepository _clubRepository;
    private readonly IClubImageService _clubImageService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;

    public CreateClubCommandHandler(
        IValidator<CreateClubCommand> validator,
        IUserRepository userRepository,
        IClubCategoryRepository clubCategoryRepository,
        IClubRepository clubRepository,
        IUnitOfWork unitOfWork,
        IClubImageService clubImageService,
        IMediator mediator,
        IUserContext userContext)
    {
        _validator = validator;
        _clubCategoryRepository = clubCategoryRepository;
        _clubRepository = clubRepository;
        _unitOfWork = unitOfWork;
        _clubImageService = clubImageService;
        _mediator = mediator;
        _userContext = userContext;
    }

    public async Task<CreateClubResponse> Handle(CreateClubCommand request, CancellationToken cancellationToken)
    {
        var userId = _userContext.GetUserId();
        
        await EnsureClubValidated(request, cancellationToken);

        var clubProfileResponse = await _clubImageService.UploadClubProfileImage(request.ClubThumbnail);

        var clubDto = new ClubDto
        {
            CreatorId = userId,
            ImageUrl = clubProfileResponse.ImageSecureUrl,
            ClubName = request.ClubName,
            ClubIntroduction = request.ClubIntroduction,
            ClubCategoryId = request.IsPrivate? null : request.ClubCategoryId,
            IsPrivate = request.IsPrivate,
        };
            
        var club = Club.IsDefault(request.PostPermission, request.InvitePermission) ? 
            Club.CreateDefault(clubDto) : 
            Club.CreateCustom(clubDto, request.PostPermission, request.InvitePermission);
        
        
        _clubRepository.AddNewClub(club);
        
        await _unitOfWork.CommitAsync(cancellationToken);

        await _mediator.Publish(new ClubCreatedEvent(club), cancellationToken);
        
        return new CreateClubResponse { Message = "new Club created", ClubId = club.ClubId };
    }   

    private async Task EnsureClubValidated(CreateClubCommand request,CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw ClubException.ClubValidationError(validationResult.Errors);
        }

        //if (!await _userRepository.CheckIdExists(request.CreatorId))
        //{
        //    throw ClubException.CreatorIdNotFound(request.CreatorId);
        //}

        if(request is { IsPrivate: false, ClubCategoryId: not null })
        {
            var categoryId = Convert.ToInt32(request.ClubCategoryId);
            if (!await _clubCategoryRepository.CheckClubCategoryExists(categoryId))
            {
                throw ClubException.ClubCategoryIdNotFound(categoryId);
            }
        }


        if (await _clubRepository.CheckClubNameExists(request.ClubName))
        {
            throw ClubException.ClubNameAlreadyExists(request.ClubName);
        }

    }
}
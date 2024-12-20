using application.EventHandlers.ArticleEventHandlers;
using application.Events.ArticleEvents;
using application.Exceptions.TagExceptions;
using application.utilities.ImageUploads.Article;
using domain.interfaces;
using FluentValidation;
using infrastructure.services.httpImgInterceptor;
using infrastructure.services.worker;
using infrastructure.UserContext;
using MediatR;

namespace application.useCases.articleInteractions.UpdateArticle;

public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand, UpdateArticleResponse>
{
    private readonly IValidator<UpdateArticleCommand> _validator;
    private readonly IUserContext _userContext;
    private readonly IArticleRepository _articleRepository;
    private readonly ITagRepository _tagRepository;
    private readonly IArticleImageService _articleImageService;
    private readonly IArticleBodyRepository _articleBodyRepository;
    private readonly IHtmlImageProcessor _htmlImageProcessor;
    private readonly IContentImageProcessor _contentImageProcessor;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;

    public UpdateArticleCommandHandler(
        IValidator<UpdateArticleCommand> validator, 
        IUserContext userContext, 
        ITagRepository tagRepository,
        IArticleRepository articleRepository, 
        IArticleImageService articleImageService,
        IArticleBodyRepository articleBodyRepository, 
        IHtmlImageProcessor htmlImageProcessor, 
        IContentImageProcessor contentImageProcessor,
        IUnitOfWork unitOfWork,
        IMediator mediator)
    {
        _validator = validator;
        _userContext = userContext;
        _tagRepository = tagRepository;
        _articleRepository = articleRepository;
        _articleImageService = articleImageService;
        _articleBodyRepository = articleBodyRepository;
        _htmlImageProcessor = htmlImageProcessor;
        _contentImageProcessor = contentImageProcessor;
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }

    public async Task<UpdateArticleResponse> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        // validate id
        var userId = _userContext.GetUserId();
        
        // skipped checks
        // club id
        // joined status
        // permissions
        
        // validate tags
        if(request.TagIds != null)
            await ValidateTags(request.TagIds);
        
        if (request.NewTags != null)
            await EnsureNewTagsAreUnique(request.NewTags);
        
        var tags = await _tagRepository.GetTagsManyAsync(request.TagIds!); 
        
        //query article
        var article = await _articleRepository.GetArticleByIdAsync(request.ArticleId)??
                      throw new KeyNotFoundException("Article not found");
        
        await _tagRepository.RemoveTagsAsync(article.ArticleId);
        
        //update article
        article.Tags = tags;
        article.ClubId = request.ClubId;
        article.ArticleTitle = request.ArticleTitle;
        
        //validate thumbnail
        string articleThumbnailUrl = "";
        try
        {
            var articleUploadResponse = await _articleImageService.UploadThumbnail(request.ArticleThumbnail!)??
                throw new Exception("Thumbnail upload failed");
            
            articleThumbnailUrl = articleUploadResponse.ImageSecureUrl;
        }
        catch (Exception ex)
        {
            articleThumbnailUrl = article.ArticleThumbnailUrl!;
        }
        article.ArticleThumbnailUrl = articleThumbnailUrl;
        
        //update time
        article.UpdateDateTime = DateTime.Now;
        
        //update draft status
        article.IsDrafted = request.IsDrafted;
        
        // update body
        var articleBody = await _articleBodyRepository.GetArticleBodyByIdAsync(request.ArticleId)??
                          throw new KeyNotFoundException("Article body not found");
        
        var (html, content) = await ProcessImages(request.ArticleHtmlContent, request.ArticleContent);

        articleBody.ArticleHtmlContent = html;
        articleBody.ArticleContent = content;
        
        await _unitOfWork.CommitAsync(cancellationToken);

        var newTagCreatedEvent = new NewTagsCreatedEvent
        {
            Article = article,
            NewTagNotification = request.NewTags != null
                ? NewTagNotification.CreateHasNewTags(request.NewTags)
                : NewTagNotification.CreateHasNoNewTags(),
        };
        
        await _mediator.Publish(newTagCreatedEvent, cancellationToken);

        return new UpdateArticleResponse
        {
            Message = "Article updated",
            ArticleId = article.ArticleId,
        };
    }
    
    private async Task ValidateTags(List<int>? requestTagIds)
    {
        if (requestTagIds == null || requestTagIds.Count == 0)
            throw new ArgumentException("No tags provided");

        if (requestTagIds is [0] && requestTagIds.Count == 1)
        {
            throw new ArgumentException("No tags provided");
        }

        if (!TagsUnique(requestTagIds))
        {
            throw TagException.DuplicateTags();
        }

        if (!await EnsureAllTagsExist(requestTagIds))
        {
            throw TagException.InvalidTagIds();
        }

    }

    private bool TagsUnique(List<int> tagIds)
    {
        HashSet<int> seedTagIds = new HashSet<int>();
        bool isUnique = true;

        foreach (var number in tagIds)
        {
            if (!seedTagIds.Add(number))
            {
                isUnique = false;
                break;
            }
        }

        return isUnique;
    }

    private async Task<bool> EnsureAllTagsExist(List<int> tagIds) 
        => await _tagRepository.AreAllIdsValidAsync(tagIds);

    private async Task EnsureNewTagsAreUnique(List<string>? newTags)
    {
        if (newTags == null) 
            throw new ArgumentException("No new tags provided");
        if (newTags.Count == 0)
            throw new ArgumentException("No tags provided");
        
        if(! await _tagRepository.AreNewTagsUniqueAsync(newTags))
            throw new InvalidOperationException("New Tags already exists");
    }

    private async Task<(string processedHtml, string processedContent)> ProcessImages(string htmlContent, string articleContent)
    {
        string processedHtml = "";
        string processedContent = "";

        try
        {
            processedHtml = await _htmlImageProcessor.ProcessHtmlContentAsync(htmlContent);
            processedContent = await _contentImageProcessor.ProcessContentAsync(articleContent);
        }
        catch (Exception ex)
        {
            processedHtml = htmlContent;
            processedContent = articleContent;
        }

        return (processedHtml, processedContent);
    }
}
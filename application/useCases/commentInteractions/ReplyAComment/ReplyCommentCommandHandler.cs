using application.utilities.UserContext;
using domain.entities;
using domain.interfaces;
using FluentValidation;
using infrastructure.services.worker;
using MediatR;

namespace application.useCases.commentInteractions.ReplyAComment;

public class ReplyCommentCommandHandler : IRequestHandler<ReplyCommentCommand, ReplyCommentResponse>
{
    private readonly IValidator<ReplyCommentCommand> _validator;
    private readonly IArticleRepository _articleRepository;
    private readonly IUserContext _userContext;
    
    private readonly ICommentRepository _commentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ReplyCommentCommandHandler(
        IValidator<ReplyCommentCommand> validator,
        ICommentRepository commentRepository,
        IUnitOfWork unitOfWork,
        IArticleRepository articleRepository, IUserContext userContext)
    {
        _validator = validator;
        _commentRepository = commentRepository;
        _unitOfWork = unitOfWork;
        _articleRepository = articleRepository;
        _userContext = userContext;
    }

    public async Task<ReplyCommentResponse> Handle(ReplyCommentCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if(!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var userId = _userContext.GetUserId(); 
        
        var article = await _articleRepository.GetArticleByIdNoTracking(request.ArticleId)??
                      throw new UnauthorizedAccessException("Article not found");

        //check if parent comment id exists
        if(! await _commentRepository.ParentCommentIdExists(request.ParentCommentId))
            throw new UnauthorizedAccessException("Parent comment not found");
        
        if(article.Archived)
            throw new UnauthorizedAccessException("Article is archived");
        
        var commentDto = new CommentDto
        {
            CommentCreatorId = userId,
            ArticleId = request.ArticleId,
            ParentCommentId = request.ParentCommentId,
            Content = request.Content,
        };
        
        var comment = Comment.Create(commentDto);
        
        _commentRepository.AddComment(comment);
        
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return new ReplyCommentResponse() { Message = $"Created reply comment: {comment.Content}" };
    }
}
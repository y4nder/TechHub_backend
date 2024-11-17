using domain.entities;
using domain.interfaces;
using FluentValidation;
using infrastructure.services.worker;
using MediatR;

namespace application.useCases.commentInteractions.CreateAComment;

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CreateCommentResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IArticleRepository _articleRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IValidator<CreateCommentCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCommentCommandHandler(
        IUserRepository userRepository,
        IArticleRepository articleRepository,
        IValidator<CreateCommentCommand> validator,
        ICommentRepository commentRepository, 
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _articleRepository = articleRepository;
        _validator = validator;
        _commentRepository = commentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateCommentResponse> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if(!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        if (! await _userRepository.CheckIdExists(request.CommentCreatorId))
            throw new UnauthorizedAccessException("User not found");
        
        var article = await _articleRepository.GetArticleByIdNoTracking(request.ArticleId)??
            throw new UnauthorizedAccessException("Article not found");

        if(article.Archived)
            throw new UnauthorizedAccessException("Article is archived");
        
        var commentDto = new CommentDto
        {
            CommentCreatorId = request.CommentCreatorId,
            ArticleId = request.ArticleId,
            ParentCommentId = null,
            Content = request.Content,
        };
        
        var comment = Comment.Create(commentDto);
        
        _commentRepository.AddComment(comment);
        
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return new CreateCommentResponse { Message = $"Created comment: {comment.Content}" };
    }
}
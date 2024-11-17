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
    private readonly IUserRepository _userRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ReplyCommentCommandHandler(
        IValidator<ReplyCommentCommand> validator,
        ICommentRepository commentRepository,
        IUnitOfWork unitOfWork,
        IUserRepository userRepository,
        IArticleRepository articleRepository)
    {
        _validator = validator;
        _commentRepository = commentRepository;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _articleRepository = articleRepository;
    }

    public async Task<ReplyCommentResponse> Handle(ReplyCommentCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if(!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        if (! await _userRepository.CheckIdExists(request.CommentCreatorId))
            throw new UnauthorizedAccessException("User not found");
        
        var article = await _articleRepository.GetArticleByIdNoTracking(request.ArticleId)??
                      throw new UnauthorizedAccessException("Article not found");

        //check if parent comment id exists
        if(! await _commentRepository.ParentCommentIdExists(request.ParentCommentId))
            throw new UnauthorizedAccessException("Parent comment not found");
        
        if(article.Archived)
            throw new UnauthorizedAccessException("Article is archived");
        
        var commentDto = new CommentDto
        {
            CommentCreatorId = request.CommentCreatorId,
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
using FluentValidation;

namespace application.useCases.commentInteractions.ReplyAComment;

public class ReplyCommentValidator : AbstractValidator<ReplyCommentCommand>
{
    public ReplyCommentValidator()
    {
        RuleFor(c => c.Content)
            .NotEmpty().WithMessage("Please specify a content for the comment reply");
    }
}
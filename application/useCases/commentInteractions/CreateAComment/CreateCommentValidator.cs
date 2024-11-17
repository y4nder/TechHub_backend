using FluentValidation;

namespace application.useCases.commentInteractions.CreateAComment;

public class CreateCommentValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentValidator()
    {
        RuleFor(c => c.Content)
            .NotEmpty().WithMessage("Please specify a content for the comment");
    }
}
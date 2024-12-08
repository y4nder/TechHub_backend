using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace application.useCases.articleInteractions.CreateArticle;

public class CreateArticleValidator : AbstractValidator<CreateArticleCommand>
{
    public CreateArticleValidator()
    {
        RuleFor(a => a.ArticleTitle)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters");
        
        RuleFor(a => a.ArticleThumbnail)
            .Must(BeAValidSize).WithMessage("Article thumbnail must be a valid size")
            .Must(HaveValidExtension!).WithMessage("Article thumbnail must be a valid extension");

        RuleFor(a => a.TagIds)
            .Must((a, tags) => AtLeastOneTag(tags, a.NewTags))
            .WithMessage("At least one TagId or NewTag is required.");

        RuleFor(a => a.ArticleContent)
            .NotEmpty().WithMessage("Body is required");
    }

    private bool AtLeastOneTag(List<int>? tagIds, List<string>? newTags)
    {
        return tagIds is { Count: > 0 } || newTags is { Count: > 0 };
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
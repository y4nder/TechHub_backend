using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace application.useCases.articleInteractions.UpdateArticle;

public class UpdateArticleValidator : AbstractValidator<UpdateArticleCommand>
{
    public UpdateArticleValidator()
    {
        RuleFor(a => a.ArticleTitle)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters");

        // Conditional validation for ArticleThumbnail
        RuleFor(a => a.ArticleThumbnail)
            .Must(BeAValidSize).WithMessage("Article thumbnail must be a valid size")
            .Must(HaveValidExtension!).WithMessage("Article thumbnail must be a valid extension")
            .When(a => a.ArticleThumbnail is not null); // Validate the file if it's provided

        // // Conditional validation for ArticleThumbnailUrl
        // RuleFor(a => a.ArticleThumbnailUrl)
        //     .Must(url => BeValidThumbnailUrl(url, a => a.ArticleThumbnail == null))
        //     .WithMessage("Article thumbnail URL is required if no thumbnail file is provided.")
        //     .When(a => a.ArticleThumbnail == null); // Only validate the URL if there's no file

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
        if (file is null)
            throw new NullReferenceException("File is null.");
        return file.Length is > 0 and <= maxFileSize;
    }

    private bool HaveValidExtension(IFormFile file)
    {
        var allowedExtensions = new[] { ".png", ".jpeg", ".jpg" };
        var fileExtension = System.IO.Path.GetExtension(file.FileName)?.ToLower();
        return allowedExtensions.Contains(fileExtension);
    }

    // Custom rule to check ArticleThumbnailUrl if no file is provided
    private bool BeValidThumbnailUrl(string? url, Func<bool> condition)
    {
        return condition() ? !string.IsNullOrEmpty(url) : true;
    }
}

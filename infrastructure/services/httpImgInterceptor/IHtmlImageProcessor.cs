using System.Text.RegularExpressions;
using HtmlAgilityPack;
using infrastructure.services.cloudinary;
using Microsoft.AspNetCore.Http;

namespace infrastructure.services.httpImgInterceptor;

public interface IHtmlImageProcessor
{
    Task<string> ProcessHtmlContentAsync(string htmlContent);
}

public class HtmlImageProcessor : IHtmlImageProcessor
{
    private readonly IImageUploader _imageUploader;

    public HtmlImageProcessor(IImageUploader imageUploader)
    {
        _imageUploader = imageUploader;
    }

    public async Task<string> ProcessHtmlContentAsync(string htmlContent)
    {
        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(htmlContent);

        var imgNodes = htmlDoc.DocumentNode.SelectNodes("//img");
        if (imgNodes == null) return htmlContent;

        foreach (var imgNode in imgNodes)
        {
            var src = imgNode.GetAttributeValue("src", null);
            if (src != null && IsBase64Image(src))
            {
                try
                {
                    // Convert Base64 to IFormFile
                    var formFile = ConvertBase64ToIFormFile(src);

                    // Upload to Cloudinary
                    var uploadResponse = await _imageUploader.UploadImageAsync(formFile, "article_images/content");

                    // Replace the src attribute with the Cloudinary URL
                    imgNode.SetAttributeValue("src", uploadResponse.ImageSecureUrl);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing image: {ex.Message}");
                }
            }
        }

        return htmlDoc.DocumentNode.OuterHtml;
    }

    private bool IsBase64Image(string src)
    {
        return src.StartsWith("data:image/");
    }

    private IFormFile ConvertBase64ToIFormFile(string base64Data, string fileNamePrefix = "image")
    {
        var base64Pattern = @"data:image\/(?<type>[a-zA-Z]+);base64,(?<data>.+)";
        var match = Regex.Match(base64Data, base64Pattern);

        if (!match.Success)
            throw new ArgumentException("Invalid base64 image data");

        var imageType = match.Groups["type"].Value;
        var imageData = match.Groups["data"].Value;

        // Determine file extension
        var fileExtension = imageType.ToLower() switch
        {
            "jpeg" => "jpg",
            "png" => "png",
            "gif" => "gif",
            _ => throw new NotSupportedException($"Unsupported image type: {imageType}")
        };

        // Decode base64 data
        var byteArray = Convert.FromBase64String(imageData);
        var stream = new MemoryStream(byteArray);

        // Use the determined file extension
        var fileName = $"{fileNamePrefix}.{fileExtension}";

        return new FormFile(stream, 0, byteArray.Length, null, fileName)
        {
            Headers = new HeaderDictionary(),
            ContentType = $"image/{imageType}"
        };
    }
}

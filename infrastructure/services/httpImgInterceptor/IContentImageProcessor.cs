using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using infrastructure.services.cloudinary;
using Microsoft.AspNetCore.Http;


namespace infrastructure.services.httpImgInterceptor
{
    public interface IContentImageProcessor
    {
        Task<string> ProcessContentAsync(string content);
    }

    public class ContentImageProcessor : IContentImageProcessor
    {
        private readonly IImageUploader _imageUploader;

        public ContentImageProcessor(IImageUploader imageUploader)
        {
            _imageUploader = imageUploader;
        }

        public async Task<string> ProcessContentAsync(string content)
        {
            // Parse the JSON string
            var jsonObject = JObject.Parse(content);

            // Get the ops array
            var opsArray = jsonObject["ops"] as JArray;

            if (opsArray != null)
            {
                foreach (var op in opsArray)
                {
                    // Look for insert objects with an image property
                    var insertObj = op["insert"];
                    if (insertObj != null && insertObj.Type == JTokenType.Object && insertObj["image"] != null)
                    {
                        var base64Image = insertObj["image"].ToString();

                        if (IsBase64Image(base64Image))
                        {
                            
                            // Convert the base64 data to IFormFile
                            var formFile = ConvertBase64ToIFormFile(base64Image);

                            // Upload the image to Cloudinary
                            var imageUploadResponse = await _imageUploader.UploadImageAsync(formFile, "article_images/content");

                            // Replace the base64 string with the Cloudinary URL
                            insertObj["image"] = imageUploadResponse.ImageSecureUrl;
                        }
                    }
                }
            }

            // Return the processed JSON as a string
            return jsonObject.ToString();
        }

        private bool IsBase64Image(string src)
        {
            return src.StartsWith("data:image/");
        }

        private string ExtractBase64Data(string src)
        {
            var base64Pattern = @"data:image\/[a-zA-Z]+;base64,(?<data>.+)";
            var match = Regex.Match(src, base64Pattern);
            return match.Success ? match.Groups["data"].Value : null;
        }

        private IFormFile ConvertBase64ToIFormFile(string base64Data, string fileNamePrefix = "image")
        {
            try
            {
                // Match the regex pattern
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

                // Decode the base64 string to byte array
                byte[] byteArray;
                try
                {
                    byteArray = Convert.FromBase64String(imageData);
                }
                catch (FormatException)
                {
                    throw new ArgumentException("Base64 string is not properly formatted.");
                }

                var stream = new MemoryStream(byteArray);

                // Create a unique filename
                var fileName = $"{fileNamePrefix}_{Guid.NewGuid()}.{fileExtension}";

                return new FormFile(stream, 0, byteArray.Length, null, fileName)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = $"image/{imageType}"
                };
            }
            catch (Exception ex)
            {
                // Log exception for debugging
                Console.WriteLine($"Error in ConvertBase64ToIFormFile: {ex.Message}");
                throw;
            }
        }
    }
}

using infrastructure.services.cloudinary;
using MediatR;

namespace application.utilities.ImageUploads;

public class ImageDeleteCommand : IRequest<ImageDeletionResponse>
{
    public string PublicId { get; set; } = string.Empty;
}

public class ImageDeletionResponse
{
    public string Message { get; set; } = null!;
}

public class ImageDeleteCommandHandler : IRequestHandler<ImageDeleteCommand, ImageDeletionResponse>
{
    private readonly IImageUploader _imageUploader;

    public ImageDeleteCommandHandler(IImageUploader imageUploader)
    {
        _imageUploader = imageUploader;
    }

    public async Task<ImageDeletionResponse> Handle(ImageDeleteCommand request, CancellationToken cancellationToken)
    {
        if(string.IsNullOrEmpty(request.PublicId))
            throw new InvalidOperationException("You must provide a valid publicId");
        
        var deleted = await  _imageUploader.DeleteImageAsync(request.PublicId);

        if(!deleted) throw new Exception("Unable to delete image");
        
        return new ImageDeletionResponse
        {
            Message = $"Image {request.PublicId} deleted"
        };
    }
}
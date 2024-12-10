using application.utilities.ImageUploads;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using web_api.utils;

namespace web_api.Controllers;

public class ImageUploadController : Controller
{
    private readonly ISender _sender;

    public ImageUploadController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("uploadImage")]
    public async Task<IActionResult> UploadImage([FromForm] ImageUploadCommand command)
    {
        try
        {
            var response = await _sender.Send(command);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return ErrorFactory.CreateErrorResponse(ex);
        }
    }
    
    [HttpDelete("deleteImage")]
    public async Task<IActionResult> UploadImage([FromBody] ImageDeleteCommand command)
    {
        try
        {
            var response = await _sender.Send(command);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return ErrorFactory.CreateErrorResponse(ex);
        }
    }
}
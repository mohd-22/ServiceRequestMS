using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceRequestMS.Application.Services.Interfaces;
using ServiceRequestMS.core.Models.Enums;
using System.Security.Claims;

namespace ServiceRequestMS.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = nameof(UserRoles.Employee))]

public class AttachmentController : ControllerBase
{
    private readonly IAttachmentService _attachmentService;
    public AttachmentController(IAttachmentService attachmentService)
    {
        _attachmentService = attachmentService;
    }

    [HttpPost("UploadAttachment/{RequestId}")]
    public async Task<IActionResult> UploadAttachment([FromRoute] Guid RequestId, IFormFile file)
    {

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (Guid.TryParse(userId, out Guid userGuid))
        {
            var result = await _attachmentService.UploadAttachment(userGuid, RequestId, file);
            if (result.Success == false) return BadRequest(result);
            return Ok(result);
        }
        return Unauthorized("User ID is not valid.");
    }

    [HttpGet("request/{requestId}")]
    public async Task<IActionResult> GetAttachmentsByRequest([FromRoute] Guid requestId)
    {
        var result = await _attachmentService.GetAttachmentsByRequest(requestId);
        if (result.Success == false) return BadRequest(result);
        return Ok(result);
    }

    [HttpGet("download/{id}")]
    public async Task<IActionResult> DownloadFile(Guid id)
    {
        var (fileBytes, contentType, fileName) = await _attachmentService.DownloadFile(id);

        if (fileBytes == null)
        {
            return NotFound("File Not Found");
        }

        return File(fileBytes, contentType, fileName);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAttachment(Guid id)
    {
        var result = await _attachmentService.DeleteAttachment(id);
        if (!result.Success) return BadRequest(result);

        return Ok(result);
    }
}


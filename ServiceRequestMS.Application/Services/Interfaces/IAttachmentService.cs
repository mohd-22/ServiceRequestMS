using Microsoft.AspNetCore.Http;
using ServiceRequestMS.Application.Common;
using ServiceRequestMS.Application.DTOs;

namespace ServiceRequestMS.Application.Services.Interfaces;
public interface IAttachmentService
{
    Task<(byte[] fileBytes, string contentType, string fileName)> DownloadFile(Guid id);
    Task<ApiResponse<IEnumerable<AttachmentDto>>> GetAttachmentsByRequest(Guid requestId);
    Task<ApiResponse<bool>> UploadAttachment(Guid userId,Guid requestId, IFormFile file);
    Task<ApiResponse<bool>> DeleteAttachment(Guid id);
}

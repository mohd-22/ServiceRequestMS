using Microsoft.AspNetCore.Hosting; 
using Microsoft.AspNetCore.Http;
using ServiceRequestMS.Application.Common;
using ServiceRequestMS.Application.DTOs;
using ServiceRequestMS.Application.Services.Interfaces;
using ServiceRequestMS.core.Models;
using ServiceRequestMS.Data.Repositories.Interfaces;
using System.Net;
using System.Security.Claims;

namespace ServiceRequestMS.Application.Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IWebHostEnvironment _env;
        private readonly IUnitOfWork _unitOfWork;
        public AttachmentService(IWebHostEnvironment env, IUnitOfWork unitOfWork)
        {
            _env = env;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse<bool>> UploadAttachment(Guid userId, Guid requestId, IFormFile file)
        {
            if(file == null)
            {
                return ApiResponse<bool>.FailureResponse("File is required");
            }
            try
            {
                string uploadFolder = Path.Combine(_env.WebRootPath, "Attachments");
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string filePath = Path.Combine(uploadFolder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                var attachment = new Attachment() {
                    FileName = fileName,
                    FilePath = Path.Combine("Attachments", fileName),
                    RequestId = requestId,
                    UserId = userId
                    
                };


                await _unitOfWork.Attachments.AddAsync(attachment);
                await _unitOfWork.CompleteAsync();  
                return ApiResponse<bool>.SuccessResponse(true, "File uploaded successfully");
            }
            catch (Exception ex) {
                throw;
            }
        }
        public async Task<ApiResponse<IEnumerable<AttachmentDto>>> GetAttachmentsByRequest(Guid requestId)
        {
            var attachments = await _unitOfWork.Attachments.FindAsNoTrackingAsync(a => a.RequestId == requestId);
            if (attachments == null) {
                return ApiResponse<IEnumerable<AttachmentDto>>.FailureResponse("No attachments found for this request");
            }
            var attachmentDtos = attachments.Select(a => new AttachmentDto
            {
                Id = a.Id,
                FileName = a.FileName,
                FilePath = a.FilePath,
            }).ToList();
            return ApiResponse<IEnumerable<AttachmentDto>>.SuccessResponse(attachmentDtos, "Attachments retrieved successfully");
            }

        public async Task<(byte[] fileBytes, string contentType, string fileName)> DownloadFile(Guid id)
        {
            var attachment = await _unitOfWork.Attachments.GetByIdAsync(id);
            if (attachment == null) return (null!, null!, null!);

            var fullPath = Path.Combine(_env.WebRootPath, attachment.FilePath);
            if (!System.IO.File.Exists(fullPath)) return (null!, null!, null!);

            var fileBytes = await System.IO.File.ReadAllBytesAsync(fullPath);
            return (fileBytes, "application/octet-stream", attachment.FileName);
        }



    }
}

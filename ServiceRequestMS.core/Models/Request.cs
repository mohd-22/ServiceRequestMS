using ServiceRequestMS.core.Models;
using ServiceRequestMS.core.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceRequestMS.Core.Models
{
    public class Request : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public RequestStatus Status { get; set; } = RequestStatus.New;
        public string? RejectionReason { get; set; }
        public Guid CategoryItemId { get; set; }
        public Item? CategoryItem { get; set; }

        [ForeignKey("CreatedBy")]
        public User? Requester { get; set; }
        public Guid? AssignedStaffId { get; set; }

        [ForeignKey("AssignedStaffId")]
        public User? AssignedStaff { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceRequestMS.Application.DTOs
{
    public class RequestAdminDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; } = string.Empty;

        public string Status { get; set; }

        public Guid RequesterId { get; set; } 
        public string RequesterName { get; set; } 

        public Guid? AssignedStaffId { get; set; }
        public string AssignedStaffName { get; set; } 


        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ItemName { get; set; }


        public DateTime CreatedDate { get; set; }


    }
}

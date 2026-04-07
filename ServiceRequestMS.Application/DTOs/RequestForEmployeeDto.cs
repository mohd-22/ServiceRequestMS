using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceRequestMS.Application.DTOs
{
    public class RequestForEmployeeDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Status { get; set; } 

        public string CategoryName { get; set; }
        public string ItemName { get; set; }

       
        public string AssignedStaffName { get; set; }

        public DateTime CreatedDate { get; set; }

        
    }
}

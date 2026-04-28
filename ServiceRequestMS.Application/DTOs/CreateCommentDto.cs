using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceRequestMS.Application.DTOs
{
    public class CreateCommentDto
    {
        public string Text { get; set; } = string.Empty;
        public Guid RequestId { get; set; }
    }
}

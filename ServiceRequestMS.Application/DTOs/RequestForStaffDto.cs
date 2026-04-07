namespace ServiceRequestMS.Application.DTOs;
public class RequestForStaffDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; } 
    public string Status { get; set; }

    public string RequesterName { get; set; }
    public string RequesterPhone { get; set; }

    public string CategoryName { get; set; }
    public string ItemName { get; set; }

    public DateTime CreatedDate { get; set; }
}

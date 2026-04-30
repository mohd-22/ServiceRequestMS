namespace ServiceRequestMS.Application.DTOs;
public class RequestForEmployeeDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string ItemName { get; set; } = string.Empty;
    public string AssignedStaffName { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }    
}

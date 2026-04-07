namespace ServiceRequestMS.Application.Common;
public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public List<string>? Errors { get; set; }

   
    public ApiResponse(T data, string message = "Success")
    {
        Success = true;
        Data = data;
        Message = message;
        Errors = null;
    }

  
    public ApiResponse(string message, List<string>? errors = null)
    {
        Success = false;
        Message = message;
        Errors = errors;
        Data = default;
    }

   
    public static ApiResponse<T> SuccessResponse(T data, string message = "Operation completed successfully")
    {
        return new ApiResponse<T>(data, message);

    }

    public static ApiResponse<T> FailureResponse(string message, List<string>? errors = null)
    {
        return new ApiResponse<T>(message, errors);

    }
}

namespace Contract.Common;

public class Response<T>
{
    public string Message { get; set; } = string.Empty;
    public bool Success { get; set; }
    public T? Data { get; set; }
    public int StatusCode { get; set; }

    public static Response<T> Ok(T data, string message = "Operation completed successfully") =>
        new() { Success = true, Data = data, Message = message, StatusCode = 200 };

    public static Response<T> Created(T data, string message = "Resource created successfully") =>
        new() { Success = true, Data = data, Message = message, StatusCode = 201 };

    public static Response<T> NotFound(string message = "Resource not found") =>
        new() { Success = false, Message = message, StatusCode = 404 };

    public static Response<T> BadRequest(string message) =>
        new() { Success = false, Message = message, StatusCode = 400 };

    public static Response<T> Unauthorized(string message = "Unauthorized") =>
        new() { Success = false, Message = message, StatusCode = 401 };

    public static Response<T> ServerError(string message = "An error occurred while processing your request") =>
        new() { Success = false, Message = message, StatusCode = 500 };
}

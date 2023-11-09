namespace TodoApp.API.Models
{
    public class ServiceResponse<T> : ServiceResponse
    {
        public T? Data { get; set; }
    }

    public class ServiceResponse
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}
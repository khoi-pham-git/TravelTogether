namespace TravelTogether2.Common
{
    public class ApiResult<T>
    {
        public ApiResult(string Code, string message, T data)
        {
            StatusCode = Code;
            Message = message;
            Data = data;
        }
        public ApiResult(string message, T data)
        {
            Message = message;
            Data = data;
        }
        public ApiResult(string Code, string message) //luan
        {
            StatusCode = Code;
            Message = message;
        }

        public string StatusCode { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }
    }
}

namespace QB.API.Models.Responses.Errors
{
    public class ErrorResponse
    {
        public ErrorResponse()
        {
        }

        public ErrorResponse(string message, int statusCode)
        {
            Message = message;
            StatusCode = statusCode;
        }

        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
}

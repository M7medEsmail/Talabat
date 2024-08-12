using System.Reflection.Metadata.Ecma335;

namespace Talabat.Errors
{
    public class ApiErrorResponse
    {
        public int? StatusCode { get; set; }
        public string Message { get; set; }


        public ApiErrorResponse(int statusCode , string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        public string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request , you are made ",
                401 => "Authorized , you are not",
                404 => "Resource Found , it was not",
                500 => "Error that are the path to dark side",
                _ => null
            };


        }
    }
}

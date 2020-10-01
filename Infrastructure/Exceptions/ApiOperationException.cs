using System;

namespace Peitho.Infrastructure.Exceptions
{
    public class ApiOperationException : Exception
    {
        public int StatusCode { get; set; }
        public string Content { get; set; }

        public ApiOperationException()
        {
        }

        public ApiOperationException(string message) : base(message)
        {
        }
        
        public ApiOperationException(string message, Exception inner) : 
            base(message, inner)
        {
        }
        
        public ApiOperationException(string message, int statusCode, string content)
            : this(message)
        {
            StatusCode = statusCode;
            Content = content;
        }
    }
}
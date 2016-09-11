using System;
using System.CodeDom.Compiler;
using System.Text;

namespace WatsonServices.Conversation.Models
{

    [GeneratedCode("NSwag", "5.5.6093.14744")]
    public class SwaggerException<TResponse> : SwaggerException
    {
        public TResponse Response { get; private set; }

        public SwaggerException(string message, string statusCode, byte[] responseData, TResponse response, Exception innerException)
            : base(message, statusCode, responseData, innerException)
        {
            Response = response;
        }
    }

    [GeneratedCode("NSwag", "5.5.6093.14744")]
    public class SwaggerException : Exception
    {
        public string StatusCode { get; private set; }

        public byte[] ResponseData { get; private set; }

        public SwaggerException(string message, string statusCode, byte[] responseData, Exception innerException)
            : base(message, innerException)
        {
            StatusCode = statusCode;
            ResponseData = responseData;
        }

        public override string ToString()
        {
            return string.Format("HTTP Response: n{0}n{1}", Encoding.UTF8.GetString(ResponseData, 0, ResponseData.Length), base.ToString());
        }
    }
}
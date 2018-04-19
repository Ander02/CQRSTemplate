using System.Net;

namespace CQRSTemplate.Infraestructure.Exceptions
{
    public class BadRequestException : BaseHttpException
    {
        public BadRequestException() : base((int)HttpStatusCode.BadRequest) { }

        public BadRequestException(dynamic body) : base((int)HttpStatusCode.BadRequest)
        {
            this.Body = body;
        }
    }
}

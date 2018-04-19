using System.Net;

namespace CQRSTemplate.Infraestructure.Exceptions
{
    public class NotFoundException : BaseHttpException
    {
        public NotFoundException() : base((int)HttpStatusCode.NotFound)
        {
        }

        public NotFoundException(dynamic body) : base((int)HttpStatusCode.NotFound)
        {
            this.Body = body;
        }
    }
}

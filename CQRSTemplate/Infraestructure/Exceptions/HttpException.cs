﻿using System;

namespace CQRSTemplate.Infraestructure.Exceptions
{
    public class BaseHttpException : Exception
    {
        public int StatusCode { get; set; }
        public dynamic Body { get; set; }

        public BaseHttpException(int statusCode)
        {
            this.StatusCode = statusCode;
        }

        public BaseHttpException(int statusCode, dynamic body) : this(statusCode)
        {
            this.Body = body;
        }
    }
}

using CQRSTemplate.Infraestructure.Exceptions;
using CQRSTemplate.Util;
using Microsoft.AspNetCore.Http;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CQRSTemplate.Infraestructure.Middlewares
{
    public class HttpExceptionHandlerMiddleware
    {
        readonly RequestDelegate next;
        public HttpExceptionHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (BaseHttpException e)
            {
                await context.MakeErrorResponse(e.StatusCode, (string)e.Body);
            }
            catch (Exception ex) when (ex.InnerException is SqlException sqlEx)
            {
                await this.HandleSqlException(sqlEx, context);
            }
            catch(Exception ex)
            {
                
            }
        }

        private async Task HandleSqlException(SqlException sqlException, HttpContext context)
        {
            switch (sqlException.Number)
            {
                case 2601:
                    await context.MakeErrorResponse(409, "Deu conflito");
                    break;
                default:
                    await context.MakeErrorResponse(500, "Vish véi, deu ruim");
                    break;
            }
        }
    }
}

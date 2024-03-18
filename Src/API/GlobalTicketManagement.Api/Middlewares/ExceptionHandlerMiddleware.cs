
using System.Net;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using GlobalTicketManagement.Application.Exceptions;


namespace GlobalTicketManagement.Api.Middlewares
{
    public class ExceptionHandlerMiddleware :IMiddleware
    {
        //private readonly RequestDelegate _next;
        //public ExceptionHandlerMiddleware(RequestDelegate next)
        //{
        //    _next = next;
        //}
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await ConvertException(context, ex);
            }
        }

        private Task ConvertException(HttpContext context, Exception ex)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

            context.Response.ContentType = "application/json";

            var result = string.Empty;

            switch (ex)
            {
                case GlobalTicketManagement.Application.Exceptions.ValidationException validationException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(validationException.ValdationErrors);
                    break;
                case BadRequestException badRequestException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    result = badRequestException.Message;
                    break;
                case NotFoundException:
                    httpStatusCode = HttpStatusCode.NotFound;
                    break;
                case Exception:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    break;
            }

            context.Response.StatusCode = (int)httpStatusCode;

            if (result == string.Empty)
            {
                result = JsonSerializer.Serialize(new { error = ex.Message });
            }

            return context.Response.WriteAsync(result);
        }
    }
}

using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace Infrastructure.Middleware
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandler> _logger = logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            ProblemDetails problem;
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                if (IsDuplicateKeyViolation(e))
                {
                    problem = new()
                    {
                        Status = (int)HttpStatusCode.BadRequest,
                        Type = "Duplicate key error",
                        Title = "Duplicate key violation",
                        Detail = "The item already exists.",
                    };
                }
                else if (e is not INonSensitiveException)
                {
                    problem = new()
                    {
                        Status = (int)HttpStatusCode.InternalServerError,
                        Type = "Server error",
                        Title = "Server error",
                        Detail = "An internal server error has occured",
                    };
                }
                else
                {
                    problem = new()
                    {
                        Status = (int)HttpStatusCode.InternalServerError,
                        Type = "Server error",
                        Title = e.GetType().Name,
                        Detail = e.Message,
                    };
                }
                string json = JsonSerializer.Serialize(problem);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json);
            }
        }

        private static bool IsDuplicateKeyViolation(Exception e)
        {
            if (e.InnerException != null)
            {
                return e.InnerException!.Message.Contains("Duplicate entry");
            }
            return false;
        }
    }
}
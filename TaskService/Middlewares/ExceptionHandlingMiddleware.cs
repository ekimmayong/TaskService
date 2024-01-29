using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using System.Net;
using TaskService.Infrastructure.Models.Exceptions;

namespace TaskService.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        public readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;

            _logger = logger;

        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                HandleConcurrencyException(context, ex);
            }
            catch(AggregateException ex)
            {
                var serviceException = ex.InnerExceptions.Select(eitem => ApplicationException(eitem)).First();
                throw serviceException;
            }
            catch(Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorResponse = new
            {
                Message = "An error occured while processing your request.",
                ExceptionMessage = exception.Message,
                ExceptionDetails = exception
            };
            _logger.LogInformation(exception, "Error Exception");

            await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse,
                Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                })
            );
        }

        private ServiceException ApplicationException(Exception ex)
        {
            if(ex is UnauthorizedAccessException exception)
            {
                return GetServiceException(exception);
            }

            if(ex is OutOfMemoryException memoryException)
            {
                return GetServiceException(memoryException);
            }

            _logger.LogInformation(ex, "Unhandled Exception");
            throw ex;
        }

        private ServiceException GetServiceException(UnauthorizedAccessException ex)
        {
            _logger.LogInformation("Unauthorized Access");
            var serviceException = new ServiceException(ex.Message, ex)
            {
                StatusCode = (int)HttpStatusCode.Forbidden
            };

            return serviceException;
        }

        private static void HandleConcurrencyException(HttpContext context, DbUpdateConcurrencyException ex)
        {
            var response = context.Response;
            response.StatusCode = (int)HttpStatusCode.Conflict;
            response.ContentType = "application/json";

            var errorMessage = new
            {
                error = "Concurrency Conflict",
                message = "Conflict Detected. Another user may have updated the data.",
                conflicDetails = ex.Entries.Select(entry => new
                {
                    entry.Entity.GetType().FullName,
                    OriginalValues = entry.OriginalValues.Properties.ToDictionary(p => p.Name, p => entry.OriginalValues[p]),
                    CurrentValues = entry.CurrentValues.Properties.ToDictionary(p => p.Name, p => entry.CurrentValues[p])
                })
            };

            var jsonResponse = JsonConvert.SerializeObject(errorMessage, Formatting.None, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });
            response.WriteAsync(jsonResponse);
        }

        private ServiceException GetServiceException(OutOfMemoryException ex)
        {
            _logger.LogInformation("Out of memory");
            var response = new ServiceException(ex.Message, ex)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };

            return response;
        }
    }
}

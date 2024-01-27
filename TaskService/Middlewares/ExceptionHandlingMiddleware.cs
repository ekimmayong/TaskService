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

            if(ex is DBConcurrencyException dBConcurrencyException)
            {
                return GetServiceException(dBConcurrencyException);
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

        private ServiceException GetServiceException(DBConcurrencyException ex)
        {
            _logger.LogInformation("Concurrency conflict occured.");
            var response = new ServiceException(ex.Message, ex)
            {
                StatusCode = (int)HttpStatusCode.Conflict,
            };

            return response;
        }
    }
}

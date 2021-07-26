
using Birthday.Domain.Shared.Exceptions;
using Birthday.PublicAPI.Controllers.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Birthday.PublicAPI.Controllers
{
    public class ApplicationExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ApplicationExceptionOptions _options;
        private readonly ILogger<ApplicationExceptionHandler> _logger;

        public ApplicationExceptionHandler(RequestDelegate next,
            IOptions<ApplicationExceptionOptions> options, ILogger<ApplicationExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
            _options = options.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DomainException e)
            {
                _logger.LogError(e, "Ошибка!");
                context.Response.StatusCode = (int)ObtainStatusCode(e);
                await context.Response.WriteAsJsonAsync(new
                {
                    TraceId = context.TraceIdentifier,
                    Message = e.Message
                }, context.RequestAborted);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Ошибка!");
                context.Response.StatusCode = _options.DefaultErrorStatusCode;
                await context.Response.WriteAsJsonAsync(new
                {
                    TraceId = context.TraceIdentifier,
                    Message = "Произошла ошибка"
                }, context.RequestAborted);
            }
        }

        private static HttpStatusCode ObtainStatusCode(DomainException domainException)
        {
            return domainException switch
            {
                NotFoundException => HttpStatusCode.NotFound,
                NoRightsException => HttpStatusCode.Forbidden,
                ConflictException => HttpStatusCode.Conflict,
                InvalidDateFormatException => HttpStatusCode.BadRequest,
                _ => throw new ArgumentOutOfRangeException(nameof(domainException), domainException, null)
            };
        }
        public class ApplicationExceptionOptions
        {
            public int DefaultErrorStatusCode { get; set; } = StatusCodes.Status500InternalServerError;
        }

    }
}

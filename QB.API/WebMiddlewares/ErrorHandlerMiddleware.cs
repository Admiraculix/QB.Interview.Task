using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QB.API.Models.Responses.Errors;
using QB.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace QB.API.WebMiddlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(
            RequestDelegate next,
            ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var errorResponse = new ErrorResponse();

                switch (exception)
                {
                    case AppException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                        errorResponse.Message = exception.Message;
                        _logger.LogError(exception, $"Application validation error . Message: {exception.Message}");
                        break;
                    case DbUpdateException e:
                        // DB Update error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        errorResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                        errorResponse.Message = "Something going wrong while updating. Please contact support";
                        _logger.LogError(exception, $"Db update operation fail. Message: {exception.Message}");
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                        errorResponse.Message = "Something going wrong. Please contact support";
                        _logger.LogError(exception, $"Unhandled error. Message: {exception.Message}");

                        break;
                }
                var result = JsonConvert.SerializeObject(errorResponse);

                await response.WriteAsync(result);
            }
        }
    }
}

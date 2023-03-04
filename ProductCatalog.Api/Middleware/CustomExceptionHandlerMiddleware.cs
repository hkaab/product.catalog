
using ProductCatalog.Infrastructure.Exceptions;
using ProductCatalog.Infrastructure.Extensions;
using ProductCatalog.Models;
using ProductCatalog.Models.Enums;
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProductCatalog.Api.Middleware;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;

    public CustomExceptionHandlerMiddleware(ILogger<CustomExceptionHandlerMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        ErrorResponse? errorResponse = null;
        var aggregatedExceptionMessage = exception.GetAggregatedExceptionMessage();
        switch (exception)
        {
            case NotFoundException notFoundException:
                _logger.LogError(exception, aggregatedExceptionMessage);
                errorResponse = new ErrorResponse(
                  notFoundException.Message,
                  ErrorPriority.HIGH,
                  ErrorCode.NotFound);
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                break;

            case InvalidOperationException invalidOperationException:
                _logger.LogError(exception, aggregatedExceptionMessage);
                errorResponse =
                  new ErrorResponse(
                    invalidOperationException.Message,
                    ErrorPriority.HIGH,
                    ErrorCode.InternalServerError);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
            case OperationCanceledException:
                _logger.LogError(exception, aggregatedExceptionMessage);
                errorResponse =
                  new ErrorResponse(
                    "The operation was cancelled",
                    ErrorPriority.HIGH,
                    ErrorCode.OperationCancelled);
                context.Response.StatusCode = (int)HttpStatusCode.NoContent;
                break;
            case ArgumentException:
            case JsonException:
                _logger.LogError(exception, aggregatedExceptionMessage);
                errorResponse = new ErrorResponse(
                  aggregatedExceptionMessage,
                  ErrorPriority.HIGH,
                  ErrorCode.InvalidData);
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            default:
                _logger.LogError(exception, aggregatedExceptionMessage);
                errorResponse =
                  new ErrorResponse(
                    "The request was not completed due to an internal error on the server side",
                    ErrorPriority.HIGH,
                    ErrorCode.InternalServerError);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        var errorResponseJson = JsonSerializer.Serialize(errorResponse,
          new JsonSerializerOptions()
          {
              PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
              DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
          });
        context.Response.ContentType = MediaTypeNames.Application.Json;

        return context.Response.WriteAsync(errorResponseJson);
    }
}
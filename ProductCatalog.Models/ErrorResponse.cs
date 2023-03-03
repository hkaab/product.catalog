using ProductCatalog.Library.Models;
using ProductCatalog.Models.Enums;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;


namespace ProductCatalog.Models;

[ExcludeFromCodeCoverage]
public class ErrorResponse
{

    public ErrorResponse()
    {
        TraceId = Guid.NewGuid().ToString();
        Timestamp = DateTime.UtcNow.ToString();
    }

    public ErrorResponse(List<Error> errors)
    {
        TraceId = Guid.NewGuid().ToString();
        Timestamp = DateTime.UtcNow.ToString();
        Errors = errors;
    }

    public ErrorResponse(string message, ErrorPriority errorPriority, ErrorCode errorCode)
    {
        TraceId = Guid.NewGuid().ToString();
        Timestamp = DateTime.UtcNow.ToString();
        Errors = new List<Error> {
    new Error
    {
      ErrorCode = errorCode,
      Message = message,
      Priority = errorPriority,
    }
  };
    }

    [DataMember]
    public string TraceId { get; set; }

    [DataMember]
    public string Timestamp { get; set; }

    [DataMember]
    public List<Error> Errors { get; set; }
}
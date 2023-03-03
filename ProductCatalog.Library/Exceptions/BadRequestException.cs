using ProductCatalog.Library.Models;
using ProductCatalog.Models.Enums;

namespace ProductCatalog.Library.Exceptions;

#pragma warning disable S3925
public class BadRequestException : Exception
{
    private const string PathSubstring = "Path: $.";

    public BadRequestException()
      : base("One or more validation failures have occurred.")
    {
        Errors = new List<Error>();
    }

    public BadRequestException(string message) : base(message)
    {
    }

    public BadRequestException(IEnumerable<string> modelErrorMessages)
      : base("One or more validation failures have occurred.")
    {
        Errors = new List<Error>();
        foreach (var modelErrorMessage in modelErrorMessages)
        {
            var startIndex = modelErrorMessage.IndexOf(PathSubstring, StringComparison.Ordinal);
            var endIndex = modelErrorMessage.IndexOf("|");
            var errorMessage = modelErrorMessage;
            if (startIndex > -1 && endIndex > -1)
            {
                startIndex += PathSubstring.Length;
                var field = modelErrorMessage.Substring(startIndex, endIndex - startIndex);
                errorMessage = $"Invalid value for the field {field.Trim()}";
            }
            var error = new Error()
            {
                Priority = ErrorPriority.HIGH,
                ErrorCode = ErrorCode.InvalidRequest,
                Message = errorMessage
            };
            Errors.Add(error);
        }
    }
    public List<Error> Errors { get; set; }
}
using System.Runtime.Serialization;

namespace ProductCatalog.Infrastructure.Exceptions;

[DataContract]
public class NotFoundException : Exception
{
    public NotFoundException(string message)
      : base(message)
    {
    }
}

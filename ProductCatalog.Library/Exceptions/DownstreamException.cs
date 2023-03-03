using System;
using System.Diagnostics.CodeAnalysis;

namespace ProductCatalog.Library.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class DownstreamException: Exception
    {
        public DownstreamException(string message) : base(message)
        {
        }
    }
}
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace ProductCatalog.Models;

[DataContract]
[ExcludeFromCodeCoverage]
public class ErrorProperties
{
    [DataMember]
    public string DownStreamError { get; set; }

    [DataMember]
    public string StackTrace { get; set; }
}
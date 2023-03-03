using System.Runtime.Serialization;
using ProductCatalog.Models.Enums;

namespace ProductCatalog.Library.Models
{
    [DataContract]
  public class Error
  {
    [DataMember]
    public ErrorCode ErrorCode { get; set; }

    [DataMember]
    public string Message { get; set; }

    [DataMember]
    public ErrorPriority Priority { get; set; }
  }
}


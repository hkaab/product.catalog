using System.Net;

namespace ProductCatalog.Models.Enums;
public enum ErrorCode
{
    MaxLengthValidation = 1101,
    RequiredFieldMissing = 1102,
    MaxOrderItems = 1103,
    MinOrderItems = 1104,
    ScalePrecisionError = 1105,
    ConditionalMandatory = 1106,
    EnumValidation = 1107,
    InvalidRequest = 1108,
    MaxSubstitutionItems = 1109,
    MinFulfilmentOrderItems = 1110,
    MinFulfilmentOrderItemItems = 1111,
    MinValueValidation = 1112,
    InvalidDate = 1113,
    InternalServerError = 1600,
    AzureStorageServiceUnavailable = 1601,
    DownStreamError = 1602,
    OperationCancelled = 1603,
    OrderIsNotAvailable = 1604,
    NotFound = HttpStatusCode.NotFound,
    InvalidData = HttpStatusCode.BadRequest,
}
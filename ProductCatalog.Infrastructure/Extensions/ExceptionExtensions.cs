namespace ProductCatalog.Infrastructure.Extensions;

public static class ExceptionExtensions
{
    public static string GetAggregatedExceptionMessage(this Exception ex)
    {
        var messages = new List<string>();

        if (ex is AggregateException aggEx)
        {
            foreach (var aggExInnerException in aggEx.InnerExceptions)
            {
                CollectInnerMessages(aggExInnerException);
            }
        }
        else
        {
            CollectInnerMessages(ex);
        }

        return string.Join("; ", messages);

        void CollectInnerMessages(Exception innerException)
        {
            while (innerException != null)
            {
                messages.Add(innerException.Message);
                innerException = innerException.InnerException;
            }
        }
    }
}
using Microsoft.AspNetCore.Http;

namespace McGurkin.Net.Http;

public static class CorrelationIdHelper
{
    public static Guid GetOrSetCorrelationId(IHeaderDictionary headers)
    {
        Guid returnValue = Guid.Empty;

        var cidString = headers
            .FirstOrDefault(h => string.Equals(h.Key, Constants.X_CORRELATION_ID, StringComparison.OrdinalIgnoreCase))
            .Value.FirstOrDefault();

        if (!string.IsNullOrWhiteSpace(cidString) && Guid.TryParse(cidString, out returnValue))
        {
            // Valid
        }
        else
        {
            // Invalid
            returnValue = Guid.NewGuid();
            headers[Constants.X_CORRELATION_ID] = returnValue.ToString();
        }

        return returnValue;
    }
}

public class CorrelationIdMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext context)
    {
        var cid = CorrelationIdHelper.GetOrSetCorrelationId(context.Request.Headers);

        // When a response is created, add or override the Correlation ID
        context.Response.OnStarting(() =>
        {
            if (!context.Response.Headers.ContainsKey(Constants.X_CORRELATION_ID))
                context.Response.Headers.Add(Constants.X_CORRELATION_ID, cid.ToString());
            return Task.CompletedTask;
        });

        // Proceed with the request pipeline
        await _next(context);
    }
}

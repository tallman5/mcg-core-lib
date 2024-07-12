using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using System.Diagnostics;

namespace McGurkin.ApplicationInsights;

[DebuggerStepThrough]
public class UnwantedTelemetryFilter(ITelemetryProcessor next) : ITelemetryProcessor
{
    private ITelemetryProcessor Next { get; set; } = next;

    public void Process(ITelemetry item)
    {
        if (item is RequestTelemetry request && request.Name != null)
        {
            if (request.Name.ToLower().EndsWith("hub"))
            {
                return;
            }
        }

        // Send everything else, null during automated tests
        if (null != item)
            Next?.Process(item);
    }
}

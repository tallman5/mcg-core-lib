namespace McGurkin.ServiceProviders
{
    public partial interface IRequest
    {
        Guid RequestId { get; set; }

        DateTimeOffset Timestamp { get; set; }
    }

    [System.Diagnostics.DebuggerStepThrough]
    public partial class Request(Guid requestId) : IRequest
    {
        public Request() : this(Guid.NewGuid()) { }

        public virtual Guid RequestId { get; set; } = requestId;

        public DateTimeOffset Timestamp { get; set; } = DateTime.Now;
    }
}

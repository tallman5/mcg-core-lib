namespace McGurkin.ServiceProviders
{
    public partial interface IRequest
    {
        Guid RequestId { get; set; }

        DateTimeOffset Timestamp { get; set; }
    }

    [System.Diagnostics.DebuggerStepThrough]
    public partial class Request : IRequest
    {
        public Request() : this(Guid.NewGuid()) { }

        public Request(Guid requestId)
        {
            RequestId = requestId;
            Timestamp = DateTime.Now;
        }

        public virtual Guid RequestId { get; set; }

        public DateTimeOffset Timestamp { get; set; }
    }
}

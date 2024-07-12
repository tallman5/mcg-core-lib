namespace McGurkin.ServiceProviders
{
    public partial interface IRequest
    {
        DateTimeOffset Timestamp { get; set; }
    }

    [System.Diagnostics.DebuggerStepThrough]
    public partial class Request() : IRequest
    {
        public DateTimeOffset Timestamp { get; set; } = DateTime.Now;
    }

    public partial interface IRequest<T> : IRequest
    {
        T Query { get; set; }
    }

    public partial class Request<T> : IRequest<T>
    {
        public virtual required T Query { get; set; }

        private readonly Request request = new();
        public DateTimeOffset Timestamp
        {
            get => request.Timestamp;
            set => request.Timestamp = value;
        }
    }
}

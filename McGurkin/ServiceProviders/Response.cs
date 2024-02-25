namespace McGurkin.ServiceProviders
{
    public partial interface IResponse
    {
        Guid RequestId { get; set; }
        Guid ResponseId { get; set; }
        ResponseType ResponseType { get; set; }
        string? SystemMessage { get; set; }
        string? UserMessage { get; set; }
    }

    public partial interface IResponse<T> : IResponse
    {
        T? Data { get; set; }
    }

    [System.Diagnostics.DebuggerStepThrough]
    public partial class Response(Guid responseId, Guid requestId) : IResponse
    {
        public void AppendMessage(Exception ex)
        {
            var message = ex.Message;
#if DEBUG
            message = ex.ToString();
#endif
            AppendMessage(message);
            ResponseType = ResponseTypes.Error;
        }

        public void AppendMessage(string format, params object[] args)
        {
            AppendMessage(string.Format(format, args));
        }

        public void AppendMessage(string message)
        {
            if (!string.IsNullOrWhiteSpace(SystemMessage))
                SystemMessage += "\r\n";
            SystemMessage += message;
        }

        public virtual Guid RequestId { get; set; } = requestId;

        public Response() : this(Guid.NewGuid(), Guid.Empty) { }

        public Response(Guid responseId) : this(responseId, Guid.Empty) { }

        public virtual Guid ResponseId { get; set; } = responseId;

        public ResponseType ResponseType { get; set; } = ResponseTypes.Success;

        public virtual string? SystemMessage { get; set; }

        public virtual string? UserMessage { get; set; }
    }

    [System.Diagnostics.DebuggerStepThrough]
    public partial class Response<T> : Response, IResponse, IResponse<T>
    {
        public virtual T? Data { get; set; }
    }
}

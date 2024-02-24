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
    public partial class Response : IResponse
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

        public virtual Guid RequestId { get; set; }

        public Response() : this(Guid.NewGuid(), Guid.Empty) { }

        public Response(Guid responseId) : this(responseId, Guid.Empty) { }

        public Response(Guid responseId, Guid requestId)
        {
            ResponseId = responseId;
            RequestId = requestId;
            ResponseType = ResponseTypes.Success;
        }

        public virtual Guid ResponseId { get; set; }

        public ResponseType ResponseType { get; set; }

        public virtual string? SystemMessage { get; set; }

        public virtual string? UserMessage { get; set; }
    }

    [System.Diagnostics.DebuggerStepThrough]
    public partial class Response<T> : Response, IResponse, IResponse<T>
    {
        public virtual T? Data { get; set; }
    }
}

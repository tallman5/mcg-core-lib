namespace McGurkin.ServiceProviders
{
    public partial interface IResponse
    {
        ResponseType ResponseType { get; set; }
        string? SystemMessage { get; set; }
        string? UserMessage { get; set; }
        List<string> Errors { get; }
    }

    [System.Diagnostics.DebuggerStepThrough]
    public partial class Response() : IResponse
    {
        public void AppendMessage(Exception ex)
        {
            AppendMessage(ex.ToString());
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

        public ResponseType ResponseType { get; set; } = ResponseTypes.Success;

        public virtual string? SystemMessage { get; set; }

        public virtual string? UserMessage { get; set; }

        public List<string> Errors { get; } = [];
    }

    public partial interface IResponse<T> : IResponse
    {
        T? Data { get; set; }
    }

    [System.Diagnostics.DebuggerStepThrough]
    public partial class Response<T> : IResponse<T>
    {
        public virtual T? Data { get; set; }

        private readonly Response response = new();

        public ResponseType ResponseType
        {
            get => response.ResponseType;
            set => response.ResponseType = value;
        }

        public string? SystemMessage
        {
            get => response.SystemMessage;
            set => response.SystemMessage = value;
        }

        public string? UserMessage
        {
            get => response.UserMessage;
            set => response.UserMessage = value;
        }

        public List<string> Errors { get; } = [];

        public static Response<T> Success(T data, string message = "") => new()
        {
            Data = data,
            UserMessage = message
        };

        public static Response<T> Error(string message, IEnumerable<string>? errors = null)
        {
            var response = new Response<T>
            {
                ResponseType = ResponseTypes.Error,
                UserMessage = message
            };

            if (errors != null)
            {
                response.Errors.AddRange(errors);
            }

            return response;
        }

    }
}

namespace McGurkin.ServiceProviders
{
    public partial interface IResponse
    {
        ResponseType ResponseType { get; set; }
        List<string> Errors { get; set; }
    }

    [System.Diagnostics.DebuggerStepThrough]
    public partial class Response() : IResponse
    {
        public ResponseType ResponseType { get; set; } = ResponseTypes.Success;

        public List<string> Errors { get; set; } = [];

        public static Response Success() => new();

        public static Response Error(IEnumerable<string>? errors = null)
        {
            var response = new Response
            {
                ResponseType = ResponseTypes.Error,
            };

            if (errors != null)
            {
                response.Errors.AddRange(errors);
            }

            return response;
        }

    }

    public interface IResponse<T> : IResponse
    {
        T? Data { get; set; }
    }

    [System.Diagnostics.DebuggerStepThrough]
    public class Response<T> : IResponse, IResponse<T>
    {
        private Response response { get; set; } = new();

        public T? Data { get; set; }

        public ResponseType ResponseType { get => response.ResponseType; set => response.ResponseType = value; }

        public List<string> Errors { get => response.Errors; set => response.Errors = value; }


        public static Response<T> Success(T data) => new()
        {
            Data = data
        };

        public static Response<T> Error(IEnumerable<string>? errors = null)
        {
            var response = new Response<T>
            {
                ResponseType = ResponseTypes.Error,
            };

            if (errors != null)
            {
                response.Errors.AddRange(errors);
            }

            return response;
        }
    }
}

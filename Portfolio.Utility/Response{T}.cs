namespace Portfolio.Utility
{
	public class Response<T> : Response, IResponse<T>
	{
		public Response(ResponseType responseType, string message) : base(responseType, message)
		{
		}

		public Response(ResponseType responseType, T data) : base(responseType)
		{
			Data = data;
		}

		public Response(T data, List<CustomValidationError> errors) : base(ResponseType.ValidationError)
		{
			Data = data;
			ValidationErrors = errors;
		}

        public T Data { get; set; }
        public List<CustomValidationError> ValidationErrors { get; set; }
    }
}

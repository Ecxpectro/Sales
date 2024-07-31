using System.Net;

namespace Sales.WEB.Repositories
{
	public class HttpResponseWrapper<T>
	{
		public HttpResponseWrapper(T? response, bool error, HttpResponseMessage httpResponseMessage)
		{
			Error = error;
			Response = response;
			HttpResponseMessage = httpResponseMessage;
		}

		public bool Error { get; set; }

		public T? Response { get; set; }

		public HttpResponseMessage HttpResponseMessage { get; set; }

		public async Task<string?> GetErrorMessageAsync()
		{
			if (!Error)
			{
				return null;
			}

			var statusCode = HttpResponseMessage.StatusCode;
			if (statusCode == HttpStatusCode.NotFound)
			{
				return "Resource not found";
			}
			else if (statusCode == HttpStatusCode.BadRequest)
			{
				return await HttpResponseMessage.Content.ReadAsStringAsync();
			}
			else if (statusCode == HttpStatusCode.Unauthorized)
			{
				return "You must log in to perform this operation.";
			}
			else if (statusCode == HttpStatusCode.Forbidden)
			{
				return "You do not have permissions to perform this operation";
			}

			return "An unexpected error has occurred";
		}
	}

}

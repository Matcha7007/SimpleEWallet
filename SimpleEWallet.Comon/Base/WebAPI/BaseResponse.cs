using System.Net;

namespace SimpleEWallet.Comon.Base.WebAPI
{
	/// <summary>
	/// Provides a base response class for a request.
	/// </summary>
	/// <typeparam name="TData">The type of the data returned.</typeparam>
	public class BaseResponse<TData> : BaseResponse
		where TData : class, new()
	{
		/// <summary>
		/// The data returned.
		/// </summary>
		public TData Data { get; set; }

		/// <summary>
		/// Provides a default constructor.
		/// </summary>
		public BaseResponse()
			: base()
		{
			Data = new TData();
		}

		/// <summary>
		/// Provides a default constructor with the data.
		/// </summary>
		public BaseResponse(TData data)
			: base()
		{
			Data = data;
		}
	}

	/// <summary>
	/// Provides a base response class for a request.
	/// </summary>
	public class BaseResponse
	{
		/// <summary>
		/// A flag that determines whether the request pass all the validation.
		/// </summary>
		public bool IsValid { get; set; }

		/// <summary>
		/// The status code of the request.
		/// </summary>
		public int StatusCode { get; set; }

		/// <summary>
		/// A message from the server to the client.
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// Provides a default constructor.
		/// </summary>
		public BaseResponse()
		{
			IsValid = true;
			Message = string.Empty;
			StatusCode = (int)HttpStatusCode.OK;
		}
	}

	/// <summary>
	/// Provides a set of extension methods for <see cref="BaseResponse"/> that extend its functionality.
	/// </summary>
	public static class BaseResponse_Extensions
	{
		/// <summary>
		/// Copies common properties from the source response to the destination response.
		/// </summary>
		/// <typeparam name="TSource">The type of the source response, derived from <see cref="BaseResponse"/>.</typeparam>
		/// <typeparam name="TDestination">The type of the destination response, derived from <see cref="BaseResponse"/>.</typeparam>
		/// <param name="response">The destination response that will receive the copied values.</param>
		/// <param name="source">The source response from which values will be copied.</param>
		/// <returns>The destination response with the copied values.</returns>
		public static TDestination CopyFrom<TSource, TDestination>(this TDestination response, TSource source)
			where TSource : BaseResponse
			where TDestination : BaseResponse
		{
			response.Message = source.Message;
			response.StatusCode = source.StatusCode;
			response.IsValid = source.IsValid;
			return response;
		}

		/// <summary>
		/// Appends the message from the source response to the destination response and updates validation and status code.
		/// </summary>
		/// <typeparam name="TDestination">The type of the destination response, derived from <see cref="BaseResponse"/>.</typeparam>
		/// <typeparam name="TSource">The type of the source response, derived from <see cref="BaseResponse"/>.</typeparam>
		/// <param name="response">The destination response that will be appended to.</param>
		/// <param name="source">The source response whose message will be appended.</param>
		/// <param name="separator">The separator to use between the existing message and the appended message. Default is a newline.</param>
		/// <returns>The destination response with the appended message.</returns>
		public static TDestination AppendFrom<TDestination, TSource>(this TDestination response, TSource source, string separator = "\n")
			where TDestination : BaseResponse
			where TSource : BaseResponse
		{
			response.AppendMessage(source.Message, separator);
			response.IsValid = response.IsValid && source.IsValid;

			if (response.StatusCode == (int)HttpStatusCode.OK)
			{
				response.StatusCode = source.StatusCode;
			}

			return response;
		}

		/// <summary>
		/// Appends the message from the source response to the destination response using <see cref="AppendFrom{TDestination, TSource}(TDestination, TSource, string)"/>.
		/// </summary>
		/// <typeparam name="TSource">The type of the source response, derived from <see cref="BaseResponse"/>.</typeparam>
		/// <typeparam name="TDestination">The type of the destination response, derived from <see cref="BaseResponse"/>.</typeparam>
		/// <param name="response">The source response that will be appended to the destination response.</param>
		/// <param name="destination">The destination response that will receive the appended message.</param>
		/// <param name="separator">The separator to use between the existing message and the appended message. Default is a newline.</param>
		/// <returns>The original source response.</returns>
		public static TSource AppendTo<TSource, TDestination>(this TSource response, TDestination destination, string separator = "\n")
			where TSource : BaseResponse
			where TDestination : BaseResponse
		{
			destination.AppendFrom(response, separator);
			return response;
		}

		/// <summary>
		/// Sets the response status code to 401 (Unauthorized).
		/// </summary>
		/// <typeparam name="TResponse">The type of the response, derived from <see cref="BaseResponse"/>.</typeparam>
		/// <param name="response">The response to set as unauthorized.</param>
		/// <returns>The response with the status code set to 401 (Unauthorized).</returns>
		public static TResponse SetUnauthorized<TResponse>(this TResponse response)
			where TResponse : BaseResponse
		{
			response.Message = "Invalid Token";
			response.IsValid = false;
			response.StatusCode = (int)HttpStatusCode.Unauthorized;
			return response;
		}

		/// <summary>
		/// Sets the validation message for the response if the current message is null or whitespace.
		/// </summary>
		/// <typeparam name="TResponse">The type of the response, derived from <see cref="BaseResponse"/>.</typeparam>
		/// <param name="response">The response to set the validation message for.</param>
		/// <param name="validationMessage">The validation message to set.</param>
		/// <returns>The response with the updated validation message.</returns>
		public static TResponse SetValidationMessage<TResponse>(this TResponse response, string validationMessage)
			where TResponse : BaseResponse
		{
			if (string.IsNullOrWhiteSpace(response.Message))
			{
				response.IsValid = false;
				response.Message = validationMessage.Trim();
			}
			return response;
		}

		/// <summary>
		/// Sets the error message for the response if the current status code is 200 (OK).
		/// </summary>
		/// <typeparam name="TResponse">The type of the response, derived from <see cref="BaseResponse"/>.</typeparam>
		/// <param name="response">The response to set the error message for.</param>
		/// <param name="errorMessage">The error message to set.</param>
		/// <returns>The response with the updated error message and status code set to 500 (Internal Server Error).</returns>
		public static TResponse SetErrorMessage<TResponse>(this TResponse response, string errorMessage)
			where TResponse : BaseResponse
		{
			if (response.StatusCode == (int)HttpStatusCode.OK)
			{
				response.StatusCode = (int)HttpStatusCode.InternalServerError;
				response.Message = errorMessage.Trim();
			}
			return response;
		}

		/// <summary>
		/// Appends a message to the current message in the response, using a specified separator.
		/// </summary>
		/// <typeparam name="TResponse">The type of the response, derived from <see cref="BaseResponse"/>.</typeparam>
		/// <param name="response">The response to append the message to.</param>
		/// <param name="message">The message to append.</param>
		/// <param name="separator">The separator to use between the current message and the appended message. Default is a newline.</param>
		/// <returns>The response with the appended message.</returns>
		public static TResponse AppendMessage<TResponse>(this TResponse response, string message, string separator = "\n")
			where TResponse : BaseResponse
		{
			if (string.IsNullOrWhiteSpace(response.Message))
			{
				response.Message = message.Trim();
			}
			else
			{
				response.Message += separator + message.Trim();
			}
			return response;
		}

		/// <summary>
		/// Appends a validation message to the current message in the response and marks it as invalid.
		/// </summary>
		/// <typeparam name="TResponse">The type of the response, derived from <see cref="BaseResponse"/>.</typeparam>
		/// <param name="response">The response to append the validation message to.</param>
		/// <param name="validationMessage">The validation message to append.</param>
		/// <param name="separator">The separator to use between the current message and the appended message. Default is a newline.</param>
		/// <returns>The response with the appended validation message.</returns>
		public static TResponse AppendValidationMessage<TResponse>(this TResponse response, string validationMessage, string separator = "\n")
			where TResponse : BaseResponse
		{
			response.IsValid = false;
			if (string.IsNullOrWhiteSpace(response.Message))
			{
				response.Message = validationMessage.Trim();
			}
			else
			{
				response.Message += separator + validationMessage.Trim();
			}
			return response;
		}

		/// <summary>
		/// Appends an error message to the current message in the response and sets the status code to 500 (Internal Server Error).
		/// </summary>
		/// <typeparam name="TResponse">The type of the response, derived from <see cref="BaseResponse"/>.</typeparam>
		/// <param name="response">The response to append the error message to.</param>
		/// <param name="errorMessage">The error message to append.</param>
		/// <param name="separator">The separator to use between the current message and the appended message. Default is a newline.</param>
		/// <returns>The response with the appended error message.</returns>
		public static TResponse AppendErrorMessage<TResponse>(this TResponse response, string errorMessage, string separator = "\n")
			where TResponse : BaseResponse
		{
			response.StatusCode = (int)HttpStatusCode.InternalServerError;
			if (string.IsNullOrWhiteSpace(response.Message))
			{
				response.Message = errorMessage.Trim();
			}
			else
			{
				response.Message += separator + errorMessage.Trim();
			}
			return response;
		}

		/// <summary>
		/// Sets the status code and message for the response if the current status code is 200 (OK).
		/// </summary>
		/// <typeparam name="TResponse">The type of the response, derived from <see cref="BaseResponse"/>.</typeparam>
		/// <param name="response">The response to set the status code and message for.</param>
		/// <param name="httpStatusCode">The HTTP status code to set.</param>
		/// <param name="message">The message to set for the response.</param>
		/// <returns>The response with the updated status code and message.</returns>
		public static TResponse SetStatusCodeAndMessage<TResponse>(this TResponse response, HttpStatusCode httpStatusCode, string message)
			where TResponse : BaseResponse => response.SetStatusCodeAndMessage((int)httpStatusCode, message);

		/// <summary>
		/// Sets the status code and message for the response if the current status code is 200 (OK).
		/// </summary>
		/// <typeparam name="TResponse">The type of the response, derived from <see cref="BaseResponse"/>.</typeparam>
		/// <param name="response">The response to set the status code and message for.</param>
		/// <param name="statusCode">The status code to set.</param>
		/// <param name="message">The message to set for the response.</param>
		/// <returns>The response with the updated status code and message.</returns>
		public static TResponse SetStatusCodeAndMessage<TResponse>(this TResponse response, int statusCode, string message)
			where TResponse : BaseResponse
		{
			if (response.StatusCode == 200)
			{
				response.StatusCode = statusCode;
				response.Message = message.Trim();
			}
			return response;
		}

		/// <summary>
		/// Determines whether the response is successful and valid, based on its status code and validation status.
		/// </summary>
		/// <typeparam name="TResponse">The type of the response, derived from <see cref="BaseResponse"/>.</typeparam>
		/// <param name="response">The response to check for success and validity.</param>
		/// <returns><see langword="true"/> if the response has a status code of 200 (OK) and is valid; otherwise, <see langword="false"/>.</returns>
		public static bool IsSuccessAndValid<TResponse>(this TResponse response)
			where TResponse : BaseResponse => response.StatusCode == 200 && response.IsValid;

	}
}
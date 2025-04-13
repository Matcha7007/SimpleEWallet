using System.Text;

namespace SimpleEWallet.Comon.Base.WebAPI
{
	/// <summary>
	/// Class.
	/// </summary>
	public class BaseActionResult
	{
		/// <summary>
		/// Gets or sets the identifier of the person who performed the action.
		/// </summary>
		public bool IsSuccess => IsValid && !IsError;
		/// <summary>
		/// Gets or sets the identifier of the person who performed the action.
		/// </summary>
		public bool IsValid { get; protected set; } = true;
		/// <summary>
		/// Gets or sets the identifier of the person who performed the action.
		/// </summary>
		public bool IsError { get; protected set; } = false;
		/// <summary>
		/// Gets or sets the identifier of the person who performed the action.
		/// </summary>
		public string UserMessage { get; protected set; } = string.Empty;
		/// <summary>
		/// Gets or sets the identifier of the person who performed the action.
		/// </summary>
		public string InternalMessage { get; protected set; } = string.Empty;
		/// <summary>
		/// Constructor.
		/// </summary>
		public BaseActionResult()
		{
		}

		#region SetValidationMessage
		/// <summary>
		/// Function.
		/// </summary>
		public void SetValidationMessage(string userMessage) => SetValidationMessage(userMessage, userMessage, false, false);
		/// <summary>
		/// Function.
		/// </summary>
		public void SetValidationMessage(string userMessage, bool appendUserMessage, bool appendInternalMessage) => SetValidationMessage(userMessage, userMessage, appendUserMessage, appendInternalMessage);
		/// <summary>
		/// Function.
		/// </summary>
		public void SetValidationMessage(string userMessage, string internalMessage, bool appendUserMessage, bool appendInternalMessage)
		{
			IsValid = false;
			SetUserMessage(userMessage, appendUserMessage);
			SetInternalMessage(internalMessage, appendInternalMessage);
		}
		#endregion

		#region SetErrorMessage
		/// <summary>
		/// Function.
		/// </summary>
		public void SetErrorMessage(string userMessage) => SetErrorMessage(userMessage, userMessage, false, false);
		/// <summary>
		/// Function.
		/// </summary>
		public void SetErrorMessage(string userMessage, bool appendUserMessage, bool appendInternalMessage) => SetErrorMessage(userMessage, userMessage, appendUserMessage, appendInternalMessage);
		/// <summary>
		/// Function.
		/// </summary>
		public void SetErrorMessage(string userMessage, string internalMessage, bool appendUserMessage, bool appendInternalMessage)
		{
			IsError = true;
			SetUserMessage(userMessage, appendUserMessage);
			SetInternalMessage(internalMessage, appendInternalMessage);
		}
		public void SetErrorMessage(Exception ex) => SetErrorMessage(ex, ex.Message);
		public void SetErrorMessage(Exception ex, string userMessage)
		{
			IsError = true;
			SetUserMessage(userMessage, false);
			Exception? loopException = ex;
			StringBuilder sb = new StringBuilder();
			string indent = string.Empty;
			while (loopException != null)
			{
				_ = sb.Append(indent);
				_ = sb.Append(loopException.Message);
				_ = sb.Append(Environment.NewLine);
				_ = sb.Append(indent);
				_ = sb.Append("Stack Trace = " + loopException.StackTrace);
				if (loopException.InnerException != null)
				{
					indent += "\t";
				}
				loopException = loopException.InnerException;
			}
			SetInternalMessage(sb.ToString(), false);
		}
		#endregion
		/// <summary>
		/// Function.
		/// </summary>
		protected void SetUserMessage(string userMessage, bool append)
		{
			if (append)
			{
				UserMessage += userMessage;
			}
			else
			{
				UserMessage = userMessage;
			}
		}
		/// <summary>
		/// Function.
		/// </summary>
		protected void SetInternalMessage(string internalMessage, bool append)
		{
			if (append)
			{
				InternalMessage += internalMessage;
			}
			else
			{
				InternalMessage = internalMessage;
			}
		}
	}
	/// <summary>
	/// Extension.
	/// </summary>
	public static class BaseActionResult_Extensions
	{
		/// <summary>
		/// Function.
		/// </summary>
		public static SourceType CopyTo<SourceType>(this SourceType source, BaseActionResult destination)
			where SourceType : BaseActionResult
		{
			if (!source.IsValid)
			{
				destination.SetValidationMessage(source.UserMessage, source.InternalMessage, false, false);
			}
			if (source.IsError)
			{
				destination.SetErrorMessage(source.UserMessage, source.InternalMessage, false, false);
			}
			return source;
		}
		/// <summary>
		/// Function.
		/// </summary>
		public static DestinationType CopyFrom<DestinationType>(this DestinationType destination, BaseActionResult source)
			where DestinationType : BaseActionResult
		{
			if (!source.IsValid)
			{
				destination.SetValidationMessage(source.UserMessage, source.InternalMessage, false, false);
			}
			if (source.IsError)
			{
				destination.SetErrorMessage(source.UserMessage, source.InternalMessage, false, false);
			}
			return destination;
		}
	}
}
